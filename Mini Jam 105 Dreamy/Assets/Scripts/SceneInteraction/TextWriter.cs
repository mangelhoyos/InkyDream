using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextWriter : MonoBehaviour
{
   public static TextWriter instance;
   private List<TextWriterSingle> textWriterSingleList;
   [SerializeField] TMP_Text drawTextBox;
   [SerializeField] TMP_Text helperText;
   public TMP_Text textBox;
   [SerializeField] GameObject storyGameObject;

   [Header("Game flow")]
   private DreamNode storyNode;
   [SerializeField] Transform deskPosition;
   [SerializeField] Transform playerTransform;
   private Vector3 lastPosition;
   private Quaternion lastRotation;
   private int actualIndex = 0;
   private bool isSitted = false;

   private void Awake()
   {
      instance = this;
      textWriterSingleList = new List<TextWriterSingle>();
   }

   public void ChangeNode(DreamNode node)
   {
      storyNode = node;
   }

   public void WriteText()
   {

      if(storyNode.fragments[actualIndex] != null)
      {
         AudioManager.instance.Play("Pagina");
         storyGameObject.SetActive(true);
         AddWriter_Static(textBox, System.Text.RegularExpressions.Regex.Unescape(storyNode.fragments[actualIndex].storyString), .05f, true);
         if(storyNode.fragments[actualIndex].hasDrawingAfter)
         {
            drawTextBox.text = "Draw "+storyNode.fragments[actualIndex].drawIndicatorString;
         }
      }
      else
      {
         GameManager.Instance.DisableCanvasWritten();
         storyGameObject.SetActive(true);
         textBox.text = "";
         GameManager.Instance.DisableWriteSprite();
      }
   }

   public void SitPlayer()
   {
      if(!isSitted)
      {
         isSitted = true;
         playerTransform.GetComponent<Rigidbody>().isKinematic = true;
         lastPosition = playerTransform.position;
         lastRotation = playerTransform.rotation;
         playerTransform.position = deskPosition.position;
         playerTransform.rotation = transform.rotation = Quaternion.Euler(0, -90, 0);
      }
   }

   private void ExitChair()
   {
      isSitted= false;
      playerTransform.GetComponent<Rigidbody>().isKinematic = false;
      playerTransform.position = lastPosition;
      playerTransform.rotation = lastRotation;
   }

   public static void AddWriter_Static(TMP_Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters)
   {
      instance.AddWriter(uiText, textToWrite, timePerCharacter, invisibleCharacters);
   }
   private void AddWriter(TMP_Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters)
   {
      textWriterSingleList.Add(new TextWriterSingle(uiText, textToWrite, timePerCharacter, invisibleCharacters));
   }

   private void Update()
   {
      for (int i = 0; i < textWriterSingleList.Count; i++)
      {
         bool destroyInstance = textWriterSingleList[i].Update();
         if (destroyInstance)
         {
            textWriterSingleList.RemoveAt(i);
            i--;
         }
      }
   }

   public void FuncionFinal()
   {
      StartCoroutine(FuncionFinalIE());
   }

   IEnumerator FuncionFinalIE()
   {
      yield return new WaitForSeconds(2);
      if(storyNode.fragments[actualIndex].hasDrawingAfter)
      {
         storyGameObject.SetActive(false);
         GameManager.Instance.EnableWriteSprite();
      }
      else
      {
         GameManager.Instance.DisableCanvasWritten();
         ExitChair();
         helperText.text = "Go to bed";
      }
      actualIndex++;
      
   }

   public void EndDrawing()
   {
      GameManager.Instance.DisableWriteSprite();
      WriteText();
   }
   
   public class TextWriterSingle
   {
      private TMP_Text uiText;
      private string textToWrite;
      private int characterIndex;
      private float timePerCharacter;
      private float timer;
      private bool invisibleCharacters;
      
      public TextWriterSingle(TMP_Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters)
      {
         this.uiText = uiText;
         this.textToWrite = textToWrite;
         this.timePerCharacter = timePerCharacter;
         this.invisibleCharacters = invisibleCharacters;
         characterIndex = 0;
      }
      public bool Update()
      {
         timer -= Time.deltaTime;
            if (timer <= 0f)
            {
               //Muestra el siguiente caracter
               timer += timePerCharacter;
               characterIndex++;
               string text = textToWrite.Substring(0, characterIndex);
               if (invisibleCharacters)
               {
                  text +="<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
               }
               uiText.text = text;

               if (characterIndex >= textToWrite.Length)
               {
                  //Revisamos si mostrar la frase completa
                  instance.FuncionFinal();
                  return true;
               }
            }
         return false;
      }
   }

}
