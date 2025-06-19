//using MyProject = Microsoft.Office.Interop.MSProject;
using Microsoft.Office.Interop.MSProject;
using ProjectList.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Office = Microsoft.Office.Core;

// TODO:  Выполните эти шаги, чтобы активировать элемент XML ленты:

// 1: Скопируйте следующий блок кода в класс ThisAddin, ThisWorkbook или ThisDocument.

//  protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
//  {
//      return new RibbonList();
//  }

// 2. Создайте методы обратного вызова в области "Обратные вызовы ленты" этого класса, чтобы обрабатывать действия
//    пользователя, например нажатие кнопки. Примечание: если эта лента экспортирована из конструктора ленты,
//    переместите свой код из обработчиков событий в методы обратного вызова и модифицируйте этот код, чтобы работать с
//    моделью программирования расширения ленты (RibbonX).

// 3. Назначьте атрибуты тегам элементов управления в XML-файле ленты, чтобы идентифицировать соответствующие методы обратного вызова в своем коде.  

// Дополнительные сведения можно найти в XML-документации для ленты в справке набора средств Visual Studio для Office.


namespace ProjectList
{
    [ComVisible(true)]
    public class RibbonList : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI ribbon;

        public RibbonList()
        {
        }

        public void OnOpenList(Office.IRibbonControl control)
        {
            //MessageBox.Show("Ага");
            //Tasks myTasks = null;
            //Task task = null;
            //List<TaskPeriod> taskPeriods = new List<TaskPeriod>();

            //Project myProject = Globals.ThisAddIn.Application.ActiveProject;
            //myTasks = Globals.ThisAddIn.Application.ActiveProject.Tasks;

            Cell cell = Globals.ThisAddIn.Application.ActiveCell;

            if (cell != null) 
            {
                Task task = cell.Task;
                if (task != null) 
                {
                    FormListPeriod formDialog = new(task);
                    formDialog.ShowDialog();

                }
            }
            else
                    MessageBox.Show("Не выбрана");
        }

        public void OnTableButton(Office.IRibbonControl control)
        {
            MessageBox.Show("Угу");
            /*
                       object missing = System.Type.Missing;
                       Word.Range currentRange = Globals.ThisAddIn.Application.Selection.Range;
                       Word.Table newTable = Globals.ThisAddIn.Application.ActiveDocument.Tables.Add(
                       currentRange, 3, 4, ref missing, ref missing);

                       // Get all of the borders except for the diagonal borders.
                       Word.Border[] borders = new Word.Border[6];
                       borders[0] = newTable.Borders[Word.WdBorderType.wdBorderLeft];
                       borders[1] = newTable.Borders[Word.WdBorderType.wdBorderRight];
                       borders[2] = newTable.Borders[Word.WdBorderType.wdBorderTop];
                       borders[3] = newTable.Borders[Word.WdBorderType.wdBorderBottom];
                       borders[4] = newTable.Borders[Word.WdBorderType.wdBorderHorizontal];
                       borders[5] = newTable.Borders[Word.WdBorderType.wdBorderVertical];

                       // Format each of the borders.
                       foreach (Word.Border border in borders)
                       {
                           border.LineStyle = Word.WdLineStyle.wdLineStyleSingle;
                           border.Color = Word.WdColor.wdColorBlue;
                       }
            */
        }

        #region Элементы IRibbonExtensibility

        public string GetCustomUI(string ribbonID)
        {
            return GetResourceText("ProjectList.RibbonList.xml");
        }

        #endregion

        #region Обратные вызовы ленты
        //Информацию о методах создания обратного вызова см. здесь. Дополнительные сведения о методах добавления обратного вызова см. по ссылке https://go.microsoft.com/fwlink/?LinkID=271226

        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            this.ribbon = ribbonUI;
        }

        #endregion

        #region Вспомогательные методы

        private static string GetResourceText(string resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            for (int i = 0; i < resourceNames.Length; ++i)
            {
                if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    using (StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                    {
                        if (resourceReader != null)
                        {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }

        #endregion
    }
}
