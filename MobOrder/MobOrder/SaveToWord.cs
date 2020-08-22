using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Forms;

namespace MobOrder
{
    public static class SaveToWord
    {
        //@".\UIResources.resx
        //private static readonly string TemplatePath = "M://vs2017/MobOrder/MobOrder/template.docx";
        private static readonly string TemplatePath = @"M:\vs2017\MobOrder\MobOrder\Resources\template.docx";
        private static readonly string Path = @"M:\vs2017\MobOrder\MobOrder\Resources\";
        private static string FileName = "template1";
        private static Word.Application app;
        private static Object missing = Type.Missing;

        private static string FinalMessage = "Done";

        private static string[] Keys =
        {
            "КОМАДА",
            "ВУСА",
            "ФИО",
            "ЗВАНИЕ",
            "ГОД РОЖДЕНИЯЯ",
            "ДОМАШНИЙ АДРЕСС",
            "МЕСТО РАБОТЫЫ",
            "ЯВИТЬСЯ ПО АДРЕСУУ",
            "Компания"

        };


        private static void MakeInsert(string Key, string Value)
        {
            Word.Find find = app.Selection.Find;
            find.Text = Key;
            find.Replacement.Text = Value;


            Object wrap = Word.WdFindWrap.wdFindContinue;
            Object replace = Word.WdReplace.wdReplaceAll;
            find.Execute(FindText: Type.Missing,
                MatchCase: false,
                MatchWholeWord: false,
                MatchWildcards: false,
                MatchSoundsLike: missing,
                MatchAllWordForms: false,
                Forward: true,
                Wrap: wrap,
                Format: false,
                ReplaceWith: missing, Replace: replace);
        }

        public static void SaveMember(string[] list)
        {
            var CurrentDate = DateTime.Now;
            string ResultFileName = $"{CurrentDate.Hour}_{CurrentDate.Minute}_{CurrentDate.Second}_{CurrentDate.Day}_{CurrentDate.Month}_{CurrentDate.Year}.docx";
            //Сохранить для печати одного человека

            /* Word.Application*/
            app = new Word.Application();
            app.Visible = false;

            File.Copy(TemplatePath, Path + ResultFileName, true);

            var doc = app.Documents.Open(Path + ResultFileName);

            for (int i = 0; i < 9; i++)
            {
                if (i == 0)
                    MakeInsert(Keys[i], list[i] + "\t");
                else
                    MakeInsert(Keys[i], list[i]);
            }

            doc.Save();
            doc.Close();
            app.Quit();

            MessageBox.Show(FinalMessage);

        }

        public static void SaveMembers(List<string[]> LM)
        {
            var CurrentDate = DateTime.Now;
            string ResultFileName = $"{CurrentDate.Hour}_{CurrentDate.Minute}_{CurrentDate.Second}_{CurrentDate.Day}_{CurrentDate.Month}_{CurrentDate.Year}.docx";

            //Список вспомогательных файлов
            List<string> FileNames = new List<string>();

            //Главный файл
            File.Copy(TemplatePath, Path + ResultFileName, true);


            //сформировали N-1 файлов
            for (int i = 1; i < LM.Count; i++)
            {
                string name = $"{i}.docx";
                File.Copy(TemplatePath, Path + name, true);
                FileNames.Add(name);
            }


            app = new Word.Application();
            app.Visible = false;


            //Заполняем главный файл первым человеком
            var MainDoc = app.Documents.Open(Path + ResultFileName);
            for (int i = 0; i < 9; i++)
            {
                if (i == 0)
                    MakeInsert(Keys[i], LM[0][i] + "\t");
                else
                    MakeInsert(Keys[i], LM[0][i]);

            }
            MainDoc.Save();
            MainDoc.Close();
            //Заполнили


            //Заполняем файлы с 1-го по N
            for (int i = 1; i < LM.Count; i++)
            {
                var doc = app.Documents.Open(Path + $"{i}.docx");

                for (int j = 0; j < 9; j++)
                {
                    if (j == 0)
                        MakeInsert(Keys[j], LM[i][j] + "\t");
                    else
                        MakeInsert(Keys[j], LM[i][j]);

                }


                doc.Save();
                doc.Close();
            }

            //Заполнили


            //Объединяем

            //Открыли главный файл
            MainDoc = app.Documents.Open(Path + ResultFileName);
            //Начинаем объединять

            //int n = 35;
            //doc.Paragraphs[n].Range.InsertFile(fileName);
            //doc.Paragraphs[n + 34].Range.InsertFile(fileName);
            //doc.Paragraphs[n + 34 + 34].Range.InsertFile(fileName);

            int n = 35;
            //int j_ = 0;

            //MainDoc.Paragraphs[n + j_ * 34].Range.InsertFile(Path + FileNames[j_]);

            for (int i = 0; i < FileNames.Count; i++)
            {
                try
                {
                    MainDoc.Paragraphs[n + i * 34].Range.InsertFile(Path + FileNames[i]);
                }
                catch (Exception)
                {
                    MessageBox.Show((n + i * 34).ToString());
                }

            }

            //Объединили
            app.Visible = false;
            MainDoc.Save();
            MainDoc.Close();
            app.Quit();

            //Удаляем вспомогательные файлы
            foreach (var filename in FileNames)
            {
                File.Delete(Path + filename);
            }

            MessageBox.Show(FinalMessage);



        }

       


    }
}
