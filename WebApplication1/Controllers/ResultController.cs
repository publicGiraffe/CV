using System;
using System.Collections.Generic;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Syncfusion.Pdf.Grid;
using System.Data;
using System.Reflection;

namespace WebApplication1.Controllers
{
    public class ResultController : Controller
    {
        Person person = new Person(PIRepository.Responses.LastOrDefault(), PhotoRepository.Responses.LastOrDefault(),
                EduRepository.Responses.ToList(), ExpRepository.Responses.ToList(), LaRepository.Responses.ToList());
        public ActionResult Export()
        {
            return View(person);
        }
        public static DataTable ObjectToData(object o)
        {
            DataTable dt = new DataTable("OutputData");

            DataRow dr = dt.NewRow();
            dt.Rows.Add(dr);

            o.GetType().GetProperties().ToList().ForEach(f =>
            {
                try
                {
                    f.GetValue(o, null);
                    dt.Columns.Add(f.Name, f.PropertyType);
                    dt.Rows[0][f.Name] = f.GetValue(o, null);
                }
                catch { }
            });
            return dt;
        }
        public static DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
        public ActionResult CreateDocument()
        {
            //Creates a new PDF document
            PdfDocument document = new PdfDocument();
            //Adds page settings
            document.PageSettings.Orientation = PdfPageOrientation.Portrait;
            document.PageSettings.Margins.All = 50;
            //Adds a page to the document
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            RectangleF bounds=new RectangleF();
            if (person.photo != null)
            {
                //Loads the image as stream
                System.Drawing.Image img = System.Drawing.Image.FromFile(@person.photo.ImagePath);
                double perc;
                perc = 200.0 / img.Width;
                float newHeight = (float)(img.Height * perc);
                FileStream imageStream = new FileStream(person.photo.ImagePath, FileMode.Open, FileAccess.Read);
                bounds = new RectangleF(250, 0, 200, newHeight);
                PdfImage image = PdfImage.FromStream(imageStream);
                //Draws the image to the PDF page
                page.Graphics.DrawImage(image, bounds);
            }
            PdfBrush solidBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));
            bounds = new RectangleF(0, bounds.Bottom + 90, graphics.ClientSize.Width, 30);
            //Draws a rectangle to place the heading in that region.
            graphics.DrawRectangle(solidBrush, bounds);
            //Creates a font for adding the heading in the page
            PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);
            //Creates a text element to add the invoice number
            PdfTextElement element = new PdfTextElement("Personal Information", subHeadingFont);
            element.Brush = PdfBrushes.White;

            //Draws the heading on the page
            PdfLayoutResult result = element.Draw(page, new PointF(10, bounds.Top + 8));
            string name = person.personalInfo.FirstName + " " + person.personalInfo.LastName;
            SizeF textSize = subHeadingFont.MeasureString(name);
            PointF textPosition = new PointF(graphics.ClientSize.Width - textSize.Width - 10, result.Bounds.Y);
            graphics.DrawString(name, subHeadingFont, element.Brush, textPosition);
            PdfFont timesRoman = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);
            //Creates text elements to add the address and draw it to the page.
            element = new PdfTextElement(System.Environment.NewLine
               + "Phonenumber: " + person.personalInfo.Phonenumber + System.Environment.NewLine, timesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(1, 0, 0));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 25));
            PdfPen linePen = new PdfPen(new PdfColor(126, 151, 173), 0.70f);
            PointF startPoint = new PointF(0, result.Bounds.Bottom + 3);
            PointF endPoint = new PointF(graphics.ClientSize.Width, result.Bounds.Bottom + 3);
            //Draws a line at the bottom of the address
            graphics.DrawLine(linePen, startPoint, endPoint);

            element = new PdfTextElement(System.Environment.NewLine
                 + "Email: " + person.personalInfo.Email + System.Environment.NewLine, timesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(1, 0, 0));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 25));
            linePen = new PdfPen(new PdfColor(126, 151, 173), 0.70f);
            startPoint = new PointF(0, result.Bounds.Bottom + 3);
            endPoint = new PointF(graphics.ClientSize.Width, result.Bounds.Bottom + 3);
            //Draws a line at the bottom of the address
            graphics.DrawLine(linePen, startPoint, endPoint);

            DateTime strDate = new DateTime(person.personalInfo.BirthDay.Year, person.personalInfo.BirthDay.Month, person.personalInfo.BirthDay.Day);
            element = new PdfTextElement(System.Environment.NewLine
                + "BirthDay: " + person.personalInfo.BirthDay.ToString("d") + System.Environment.NewLine, timesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(1, 0, 0));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 25));
            linePen = new PdfPen(new PdfColor(126, 151, 173), 0.70f);
            startPoint = new PointF(0, result.Bounds.Bottom + 3);
            endPoint = new PointF(graphics.ClientSize.Width, result.Bounds.Bottom + 3);
            //Draws a line at the bottom of the address
            graphics.DrawLine(linePen, startPoint, endPoint);

            element = new PdfTextElement(System.Environment.NewLine
            + "Location: " + person.personalInfo.City + System.Environment.NewLine, timesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(1, 0, 0));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 25));
            linePen = new PdfPen(new PdfColor(126, 151, 173), 0.70f);
            startPoint = new PointF(0, result.Bounds.Bottom + 3);
            endPoint = new PointF(graphics.ClientSize.Width, result.Bounds.Bottom + 3);
            //Draws a line at the bottom of the address
            graphics.DrawLine(linePen, startPoint, endPoint);

            if (!person.personalInfo.Skills.Contains("Enter text here..."))
            {
                element = new PdfTextElement(System.Environment.NewLine
            + "Skills: " + person.personalInfo.Skills + System.Environment.NewLine, timesRoman);
                element.Brush = new PdfSolidBrush(new PdfColor(1, 0, 0));
                result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 25));
                linePen = new PdfPen(new PdfColor(126, 151, 173), 0.70f);
                startPoint = new PointF(0, result.Bounds.Bottom + 3);
                endPoint = new PointF(graphics.ClientSize.Width, result.Bounds.Bottom + 3);
                //Draws a line at the bottom of the address
                graphics.DrawLine(linePen, startPoint, endPoint);
            }

            DataTable Details;
            PdfGrid grid;
            PdfGridCellStyle cellStyle;
            PdfGridRow header;
            PdfGridCellStyle headerStyle = new PdfGridCellStyle();
            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
            PdfGridLayoutResult gridResult;

            if (person.gainedEducation!=null && person.gainedEducation.Count>0)
            {
                //Creates the datasource for the table
                Details = new DataTable();
                Details = CreateDataTable(person.gainedEducation);
                //Creates a PDF grid
                grid = new PdfGrid();
                //Adds the data source
                grid.DataSource = Details;
                //Creates the grid cell styles
                cellStyle = new PdfGridCellStyle();
                cellStyle.Borders.All = PdfPens.White;
                header = grid.Headers[0];
                //Creates the header style
                headerStyle = new PdfGridCellStyle();
                headerStyle.Borders.All = new PdfPen(new PdfColor(126, 151, 173));
                headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));
                headerStyle.TextBrush = PdfBrushes.White;
                headerStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14f, PdfFontStyle.Regular);

                //Adds cell customizations
                for (int i = 0; i < header.Cells.Count; i++)
                {
                    if (i == 0 || i == 1)
                        header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                    else
                        header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
                }

                //Applies the header style
                header.ApplyStyle(headerStyle);
                cellStyle.Borders.Bottom = new PdfPen(new PdfColor(217, 217, 217), 0.70f);
                cellStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f);
                cellStyle.TextBrush = new PdfSolidBrush(new PdfColor(131, 130, 136));
                //Creates the layout format for grid
                layoutFormat = new PdfGridLayoutFormat();
                // Creates layout format settings to allow the table pagination
                layoutFormat.Layout = PdfLayoutType.Paginate;
                //Draws the grid to the PDF page.
                gridResult = grid.Draw(page, new RectangleF
                    (new PointF(0, result.Bounds.Bottom + 40), new SizeF(graphics.ClientSize.Width, graphics.ClientSize.Height - 50)),
                    layoutFormat);
                page = document.Pages.Add();
                element = new PdfTextElement(" ",subHeadingFont);
                element.Brush = PdfBrushes.White;
                graphics = page.Graphics;
                result = element.Draw(page, new PointF(10, 10));
            }
            if (person.Languages != null && person.Languages.Count > 0)
            {

                Details = new DataTable();

                //Creates the datasource for the table
                Details = CreateDataTable(person.Languages);
                //Creates a PDF grid
                grid = new PdfGrid();
                //Adds the data source
                grid.DataSource = Details;
                //Creates the grid cell styles
                cellStyle = new PdfGridCellStyle();
                cellStyle.Borders.All = PdfPens.White;
                header = grid.Headers[0];
                //Creates the header style
                headerStyle = new PdfGridCellStyle();
                headerStyle.Borders.All = new PdfPen(new PdfColor(126, 151, 173));
                headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));
                headerStyle.TextBrush = PdfBrushes.White;
                headerStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14f, PdfFontStyle.Regular);

                //Adds cell customizations
                for (int i = 0; i < header.Cells.Count; i++)
                {
                    if (i == 0 || i == 1)
                        header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                    else
                        header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
                }

                //Applies the header style
                header.ApplyStyle(headerStyle);
                cellStyle.Borders.Bottom = new PdfPen(new PdfColor(217, 217, 217), 0.70f);
                cellStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f);
                cellStyle.TextBrush = new PdfSolidBrush(new PdfColor(131, 130, 136));
                //Creates the layout format for grid
                layoutFormat = new PdfGridLayoutFormat();
                // Creates layout format settings to allow the table pagination
                layoutFormat.Layout = PdfLayoutType.Paginate;
                //Draws the grid to the PDF page.
                gridResult = grid.Draw(page, new RectangleF
                    (new PointF(0, result.Bounds.Bottom + 40), new SizeF(graphics.ClientSize.Width, graphics.ClientSize.Height - 50)),
                    layoutFormat);
                page = document.Pages.Add();
                element = new PdfTextElement(" ", subHeadingFont);
                element.Brush = PdfBrushes.White;
                graphics = page.Graphics;
                result = element.Draw(page, new PointF(10, 10));

            }

            if (person.gainedExperience != null && person.gainedExperience.Count>0)
            {
                Details = new DataTable();

                //Creates the datasource for the table
                Details = CreateDataTable(person.gainedExperience);
                //Creates a PDF grid
                grid = new PdfGrid();
                //Adds the data source
                grid.DataSource = Details;
                //Creates the grid cell styles
                cellStyle = new PdfGridCellStyle();
                cellStyle.Borders.All = PdfPens.White;
                header = grid.Headers[0];
                //Creates the header style
                headerStyle = new PdfGridCellStyle();
                headerStyle.Borders.All = new PdfPen(new PdfColor(126, 151, 173));
                headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));
                headerStyle.TextBrush = PdfBrushes.White;
                headerStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14f, PdfFontStyle.Regular);

                //Adds cell customizations
                for (int i = 0; i < header.Cells.Count; i++)
                {
                    if (i == 0 || i == 1)
                        header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                    else
                        header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
                }

                //Applies the header style
                header.ApplyStyle(headerStyle);
                cellStyle.Borders.Bottom = new PdfPen(new PdfColor(217, 217, 217), 0.70f);
                cellStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f);
                cellStyle.TextBrush = new PdfSolidBrush(new PdfColor(131, 130, 136));
                //Creates the layout format for grid
                layoutFormat = new PdfGridLayoutFormat();
                // Creates layout format settings to allow the table pagination
                layoutFormat.Layout = PdfLayoutType.Paginate;
                //Draws the grid to the PDF page.
                gridResult = grid.Draw(page, new RectangleF
                    (new PointF(0, result.Bounds.Bottom + 40), new SizeF(graphics.ClientSize.Width, graphics.ClientSize.Height - 50)),
                    layoutFormat);
            }



            //FileStream fileStream = new FileStream("Sample.pdf", FileMode.CreateNew, FileAccess.ReadWrite);
            ////Save and close the PDF document 
            //document.Save(fileStream);
            ////document.Close(true);
            MemoryStream stream = new MemoryStream();

            document.Save(stream);

            //If the position is not set to '0' then the PDF will be empty.
            stream.Position = 0;

            //Download the PDF document in the browser.
            FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");
            fileStreamResult.FileDownloadName = "YourCV.pdf";
            return fileStreamResult;




            ////Create a new PDF document
            //PdfDocument document = new PdfDocument();

            ////Add a page to the document
            //PdfPage page = document.Pages.Add();

            ////Create PDF graphics for the page
            //PdfGraphics graphics = page.Graphics;

            ////Set the standard font
            //PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

            ////Draw the text
            //graphics.DrawString("Hello World!!!", font, PdfBrushes.Black, new PointF(0, 0));

            ////Saving the PDF to the MemoryStream
            //MemoryStream stream = new MemoryStream();

            //document.Save(stream);

            ////If the position is not set to '0' then the PDF will be empty.
            //stream.Position = 0;

            ////Download the PDF document in the browser.
            //FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");
            //fileStreamResult.FileDownloadName = "YourCV.pdf";
            //return fileStreamResult;

        }

    }
}