using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Word = Microsoft.Office.Interop.Word;

namespace Komunalka
{
    public class PrintService
    {
        #region Конструктор

        public PrintService(IEnumerable<DocumentsData> dataCollection, string path, string name)
        {
            var wordApp = new Word.Application
            {
                Visible = false
            };
            try
            {
                var wordDocument = wordApp.Documents.Open(path, ReadOnly: false);
                wordDocument.Activate();
                foreach (var item in dataCollection)
                {
                    ReplaceWordStub(item.Label, item.Data, wordApp);
                }

                var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documents");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                var savePath = dir + @"\" + name + ".docx";
                wordDocument.SaveAs(savePath);
                wordApp.Visible = true;
            }
            catch (Exception e)
            {
                wordApp.Quit();
            }
        }

        #endregion

        #region Методы

        private void ReplaceWordStub(object stubToReplace, object text, Word.Application wordDocument)
        {
            object matchCase = false;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;
            //execute find and replace
            wordDocument.Selection.Find.Execute(ref stubToReplace, ref matchCase, ref matchWholeWord,
                ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref text, ref replace,
                ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);
        }

        #endregion
    }
}
