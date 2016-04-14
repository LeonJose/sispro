using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.Windows.Forms;
using System.Printing;

public class Ticket
    {
        
        
        ArrayList datos = new ArrayList();
        ArrayList footer = new ArrayList();
        
        private Image headerImage = null;
        //private Image footerImage = null;
        public ArrayList titulo = new ArrayList();
        
        int count = 0;
        
        int maxChar = 35;
        int maxCharDescription = 20;

        int imageHeight = 0;
        float leftMargin = 0;
        float topMargin = 3;

        string fontName = "Comic Sans";
        int fontSize = 12;

        Font printFont = null;
        //SolidBrush myBrush = new SolidBrush(Color.Black);

        Graphics gfx = null;

        string line = null;

        public Ticket()
        {

        }

        public Image HeaderImage
        {
            get { return headerImage; }
            set { if (headerImage != value) headerImage = value; }
        }/*
        public Image FooterImage
        {
            get { return footerImage; }
            set { if (footerImage != value) footerImage = value; }
        }*/

        public int MaxChar
        {
            get { return maxChar; }
            set { if (value != maxChar) maxChar = value; }
        }

        public int MaxCharDescription
        {
            get { return maxCharDescription; }
            set { if (value != maxCharDescription) maxCharDescription = value; }
        }

        public int FontSize
        {
            get { return fontSize; }
            set { if (value != fontSize) fontSize = value; }
        }

        public string FontName
        {
            get { return fontName; }
            set { if (value != fontName) fontName = value; }
        }

        
        public void AddTitulo(string line)
        {
            titulo.Add(line);
            
        }

        

        public void AddDatos(string dato1, string dato2)
        {
            OrderTotal newDato = new OrderTotal('?');
            datos.Add(newDato.GenerateTotal(dato1, dato2));
        }

        public void AddFooter(string dato1, string dato2)
        {
            OrderTotal newDato = new OrderTotal('?');
            footer.Add(newDato.GenerateTotal(dato1, dato2));
        }
        
        private string AlignRightText(int lenght)
        {
            string espacios = "";
            int spaces = maxChar - lenght;
            for (int x = 0; x < spaces; x++)
                espacios += " ";
            return espacios;
        }

        private string AlignCenterText(int lenght)
        {
            string espacios = "";
            int spaces = 25 - lenght;
            for (int x = 0; x < spaces; x++)
                espacios += " ";
            return espacios;
        }

        private string DottedLine()
        {
            string dotted = "";
            for (int x = 0; x < maxChar; x++)
                dotted += "=";
            return dotted;
        }

        public bool PrinterExists(string impresora)
        {
            foreach (String strPrinter in PrinterSettings.InstalledPrinters)
            {
                if (impresora == strPrinter)
                    return true;
            }
            return false;
        }

        public void PrintTicket(string impresora)
        {
            printFont = new Font(fontName, fontSize, FontStyle.Regular);
            PrintDocument pr = new PrintDocument();
            pr.PrinterSettings.PrinterName = impresora;
            pr.PrintPage += new PrintPageEventHandler(pr_PrintPage);
            
            //pr.DefaultPageSettings.PaperSize.RawKind = 11;
            /*PaperSize paperSize = new PaperSize("MyCustomSize", 10000, 10000); //numbers are optional

            paperSize.RawKind = (int)PaperKind.Custom;

            pr.DefaultPageSettings.PaperSize = paperSize;*/

            //pr.DefaultPageSettings.PaperSize = new PaperSize("thermal", 315, 5000);
            
            /*PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
            printPreviewDialog1.Document = pr;
            printPreviewDialog1.ShowDialog();
            */
            pr.Print();
        }
        private void pr_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            gfx = e.Graphics;
            
            
            /*DrawHeader();
            DrawTelefono();*/
            
            
            DrawImage();
           /* DrawSubHeader();
            DrawItems();
            */
            DrawDatos();
            DrawTitulo();/*
            DrawSaldo();
            DrawEntregado();*/
            DrawFooter();
            //DrawImageDown();
            /*if (headerImage != null)
            {
                HeaderImage.Dispose();
                headerImage.Dispose();
            }*/
        }
        
        private float YPosition()
        {
            return topMargin + (count * printFont.GetHeight(gfx) + imageHeight);
        }

        private void DrawImage()
        {
            if (headerImage != null)
            {
                try
                {
                    gfx.DrawImage(headerImage, (int)leftMargin+4, (int)YPosition(), 65, 30);
                    double height = ((double)headerImage.Height / 58)*3;
                    imageHeight = (int)Math.Round(height)+3;
                }
                catch (Exception)
                {
                }
            }
        }/*
        private void DrawImageDown()
        {

            if (footerImage != null)
            {
                try
                {
                    gfx.DrawImage(footerImage, (int)leftMargin+9, (int)YPosition()-4, 50,50);
                    
                    double height = ((double)footerImage.Height / 58) * 3;
                    imageHeight = (int)Math.Round(height) + 3;

                }
                catch (Exception)
                {
                }
            }
        }
    */
        
        private void DrawTitulo()
        {
            foreach(string tit in titulo)
            if (tit.Length > maxChar)
                {
                    int currentChar = 0;
                    int headerLenght = tit.Length;

                    while (headerLenght > maxChar)
                    {
                        line = tit.Substring(currentChar, maxChar);
                        gfx.DrawString("    "+line, new Font("Lucida Console",32,FontStyle.Bold), Brushes.Black, leftMargin+5, YPosition(), new StringFormat());

                        count++;
                        currentChar += maxChar;
                        headerLenght -= maxChar;
                    }
                    line = tit;
                    gfx.DrawString(line.Substring(currentChar, line.Length - currentChar), new Font("Lucida Console", 32, FontStyle.Bold), Brushes.Black, leftMargin+5, YPosition(), new StringFormat());
                    count++;
                }
                else
                {
                    line = tit;
                    gfx.DrawString(line, new Font("Comic Sans", 32, FontStyle.Bold), Brushes.Black, leftMargin+5, YPosition(), new StringFormat());
                    
                    count++;
                }
            
            DrawEspacio();
            DrawEspacio(); 
            DrawEspacio();
        }
    
        
        private void DrawDatos()
        {
            OrderTotal ordTot = new OrderTotal('?');

            foreach (string dato in datos)
            {
                if (dato.Length > maxChar)
                {
                    int currentChar = 0;
                    int headerLenght = dato.Length;
                    string aa = ordTot.GetTotalCantidad(dato);
                    string bb = "     " + ordTot.GetTotalName(dato);
                    string cc;
                    if (bb != "")
                        cc = aa + bb;
                    else
                        cc = "       " + aa + bb;
                    while (headerLenght > maxChar)
                    {

                        line = cc.Substring(currentChar, maxChar);
                        line = AlignRightText(line.Length) + line;
                        
                        gfx.DrawString(line, printFont, Brushes.Black, leftMargin, YPosition(), new StringFormat());
                        leftMargin = 0;

                        
                        count++;

                        currentChar += maxChar;
                        headerLenght -= maxChar;
                    }
                    line = cc;
                    gfx.DrawString(line.Substring(currentChar, line.Length - currentChar), printFont, Brushes.Black, leftMargin, YPosition(), new StringFormat());
                    count++;
                }
                else
                {
                    string aa = ordTot.GetTotalCantidad(dato);
                    string bb = ordTot.GetTotalName(dato);
                    string cc;
                    if (bb != "")
                        cc = aa + bb;
                    else
                        cc = "       " + aa + bb;
                    line = cc;
                    gfx.DrawString(line, printFont, Brushes.Black, leftMargin, YPosition(), new StringFormat());

                    count++;
                }
                
            }
            leftMargin = 0;
            DrawEspacio();


            line = DottedLine();

            gfx.DrawString(line, printFont, Brushes.Black, leftMargin, YPosition(), new StringFormat());

            count++;
            DrawEspacio();
        }


        private void DrawFooter()
        {
            OrderTotal ordTot = new OrderTotal('?');

            foreach (string l in footer)
            {
                line = ordTot.GetTotalCantidad(l);
                line = AlignRightText(line.Length) + line;

                gfx.DrawString(line, printFont, Brushes.Black, leftMargin, YPosition(), new StringFormat());
                leftMargin = 0;

                line = "" + ordTot.GetTotalName(l);
                gfx.DrawString(line, printFont, Brushes.Black, leftMargin, YPosition(), new StringFormat());
                count++;
            }
            leftMargin = 0;
            DrawEspacio();


            line = DottedLine();

            gfx.DrawString(line, printFont, Brushes.Black, leftMargin, YPosition(), new StringFormat());

            count++;
            DrawEspacio();
        }
        

        private void DrawEspacio()
        {
            line = "";

            gfx.DrawString(line, printFont, Brushes.Black , leftMargin, YPosition(), new StringFormat());

            count++;
        }

        
    }

    public class OrderItem
    {
        char[] delimitador = new char[] { '?' };

        public OrderItem(char delimit)
        {
            delimitador = new char[] { delimit };
        }

        public string GetItemCantidad(string orderItem)
        {
            string[] delimitado = orderItem.Split(delimitador);
            return delimitado[0];
        }

        public string GetItemName(string orderItem)
        {
            string[] delimitado = orderItem.Split(delimitador);
            return delimitado[1];
        }

        public string GetItemPrice(string orderItem)
        {
            string[] delimitado = orderItem.Split(delimitador);
            return delimitado[2];
        }
        public string GetItemSaldo(string orderItem)
        {
            string[] delimitado = orderItem.Split(delimitador);
            return delimitado[3];
        }

        public string GetItemPrices(string orderItem)
        {
            string[] delimitado = orderItem.Split(delimitador);
            return delimitado[3];
        }
        public string GetItemAnotacion(string orderItem)
        {
            string[] delimitado = orderItem.Split(delimitador);
            return delimitado[2];
        }
        public string GenerateItems(string cantidad, string itemName, string price)
        {
            return cantidad + delimitador[0] + itemName + delimitador[0] + price;
        }

        public string GenerateItem(string cantidad, string itemName, string anotacion, string price)
        {
            return cantidad + delimitador[0] + itemName + delimitador[0] + anotacion+delimitador[0]+ price;
        }
        public string GenerateItem2(string prenda, string cantidad, string precio, string saldo)
        {
            return prenda + delimitador[0] + cantidad + delimitador[0] + precio + delimitador[0] + saldo;
        }

        public string GenerateItem3(string prenda, string cantidad, string precio, string saldo)
        {
            return prenda + delimitador[0] + cantidad + delimitador[0] + precio + delimitador[0] + saldo;
        }
    }

    public class OrderTotal
    {
        char[] delimitador = new char[] { '?' };

        public OrderTotal(char delimit)
        {
            delimitador = new char[] { delimit };
        }

        public string GetTotalName(string totalItem)
        {
            string[] delimitado = totalItem.Split(delimitador);
            return delimitado[0];
        }

        public string GetTotalCantidad(string totalItem)
        {
            string[] delimitado = totalItem.Split(delimitador);
            return delimitado[1];
        }

        public string GenerateTotal(string totalName, string price)
        {
            return totalName + delimitador[0] + price;
        }

          

    }

