using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;
using MVC_Task1.Data;
using MVC_Task1.Models;
using System.Reflection.Metadata;

namespace MVC_Task1.Controllers
{
    public class EmpController : Controller
    {
        ApplicationDbContext db;
        public EmpController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Emp e)
        {

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "OfferLetters", $"{e.Name}_OfferLetter.pdf");
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                PdfWriter writer = new PdfWriter(fileStream);
                PdfDocument pdfDocument = new PdfDocument(writer);
                var document = new iText.Layout.Document(pdfDocument);


                document.Add(new Paragraph("Masstech Business Solutions Pvt. Ltd.")
                    .SetFontSize(24)
                    .SetBold()
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontColor(ColorConstants.BLUE));

                document.Add(new Paragraph("Official Offer Letter")
                    .SetFontSize(18)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetMarginBottom(30));


                document.Add(new Paragraph($"Date: {DateTime.Now:MMMM dd, yyyy}")
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetFontSize(12));


                document.Add(new Paragraph($"Dear {e.Name},")
                    .SetFontSize(14)
                    .SetBold()
                    .SetMarginTop(20));


                document.Add(new Paragraph("We are pleased to offer you the position of:")
                    .SetFontSize(12));
                document.Add(new Paragraph(e.Designation)
                    .SetFontSize(14)
                    .SetBold()
                    .SetMarginBottom(10));

                document.Add(new Paragraph($"Your joining date is {e.Doj:dddd, MMMM dd, yyyy}.")
                    .SetFontSize(12));

                document.Add(new Paragraph($"The offered annual salary is INR {e.salary:N2}.")
                    .SetFontSize(12)
                    .SetMarginBottom(20));


                document.Add(new Paragraph("We are confident that you will be a valuable addition to our team. " +
                                           "Your responsibilities and deliverables will be communicated to you upon joining.")
                    .SetFontSize(12)
                    .SetMarginBottom(20));

                document.Add(new LineSeparator(new SolidLine()).SetMarginTop(20).SetMarginBottom(10));

                document.Add(new Paragraph("Best Regards,")
                    .SetFontSize(12));
                document.Add(new Paragraph("HR Department")
                    .SetFontSize(12)
                    .SetBold());

                document.Add(new Paragraph("This offer is subject to company policies and verification of your credentials.")
                    .SetFontSize(10)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetMarginTop(30)
                    .SetFontColor(ColorConstants.GRAY));
                document.Close();
            }

                var emp = new Emp
                {
                    Name = e.Name,
                    Designation = e.Designation,
                    salary = e.salary,
                    Doj = e.Doj,
                    offer_letter = $"~/OfferLetters/" + e.Name + "_OfferLetter.pdf"

                };


                db.emps.Add(emp);
                db.SaveChanges();

                return RedirectToAction("OfferLetters");
            
        }

            public IActionResult OfferLetters()
            {
                var emp = db.emps.ToList();
                return View(emp);
            }
        
    }
}
