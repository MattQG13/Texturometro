#define OpenSource

using System;
using System.IO;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;
using System.Text;
using ProdutoTexturometro;
using ClassesSuporteTexturometro;
using DadosDeEnsaio;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using CsvHelper;
using CsvHelper.Configuration;
#if OpenSource
using ClosedXML.Excel;
#else
using OfficeOpenXml;
#endif
using Color = System.Drawing.Color;
using Image = System.Drawing.Image;
using System.Runtime.InteropServices.WindowsRuntime;

namespace ExportacaoResultado {

#if !OpenSource
    public class ExportacaoExcel {


        public ExportacaoExcel() {
            
        }

        public static void exportarExcel(in CorpoDeProva corpoDeProva,in DataTest Test) {

            ExcelPackage.LicenseContext=OfficeOpenXml.LicenseContext.NonCommercial;

            var dados = corpoDeProva.Resultado.GetTable();
                       
            using(var package = new ExcelPackage()) {

                SaveFileDialog SalvarArquivo = new SaveFileDialog();
                SalvarArquivo.Filter="Excel Files|*.xlsx";

                SalvarArquivo.DefaultExt="*.xlsx";
                SalvarArquivo.Title="Exportar resultado";

                var worksheet = package.Workbook.Worksheets.Add("Dados do Ensaio");

                worksheet.Cells[1,1].Value="Tempo (s)";
                worksheet.Cells[1,2].Value="Carga (g)";
                worksheet.Cells[1,3].Value="Posicao (mm)";

                int linha = 2;

                foreach(var row in dados) {
                    worksheet.Cells[linha,1].Value=row.Z;
                    worksheet.Cells[linha,2].Value=row.X;
                    worksheet.Cells[linha,3].Value=row.Y;

                    linha++;
                }
                if(Test.Tipo==TipoDeTeste.TPA) {
                    ResultadosTPA resTPA = ResultadosTPA.CalcTPA(corpoDeProva.Resultado,Test.ValorDeteccao);

                    worksheet.Cells[1,5].Value="Altura(mm)";
                    worksheet.Cells[2,5].Value=resTPA.TamProd;
                    worksheet.Cells[1,6].Value="Dureza(g)";
                    worksheet.Cells[2,6].Value=resTPA.Hardness;
                    worksheet.Cells[1,7].Value="Elasticidade(%)";
                    worksheet.Cells[2,7].Value=resTPA.Springiness; 
                    worksheet.Cells[2,7].Style.Numberformat.Format="0.00%";
                    worksheet.Cells[1,8].Value="Coesividade(%)";
                    worksheet.Cells[2,8].Value=resTPA.Cohesiveness;
                    worksheet.Cells[2,8].Style.Numberformat.Format="0.00%";
                    worksheet.Cells[1,9].Value="Resiliência(%)";
                    worksheet.Cells[2,9].Value=resTPA.Resilience;
                    worksheet.Cells[2,9].Style.Numberformat.Format="0.00%";
                    worksheet.Cells[1,10].Value="Adesividade(g.s)";
                    worksheet.Cells[2,10].Value=resTPA.Adhesiveness;
                    worksheet.Cells[1,11].Value="Gomosidade";
                    worksheet.Cells[2,11].Value = resTPA.Gumminess;
                    worksheet.Cells[1,12].Value="Mastigabilidade";
                    worksheet.Cells[2,12].Value=resTPA.Chewiness;
                }


                var res = SalvarArquivo.ShowDialog();
                
                if (res == DialogResult.OK) {
                    FileInfo file = new FileInfo(SalvarArquivo.FileName);
                    package.SaveAs(file);
                }
                
            }
        }
    }
#else
    public class ExportacaoExcel {


        public ExportacaoExcel() {

        }

        public static bool exportarExcel(in CorpoDeProva corpoDeProva,in DataTest Test) {


            var dados = corpoDeProva.Resultado.GetTable();

            SaveFileDialog SalvarArquivo = new SaveFileDialog();
            SalvarArquivo.Filter="Excel Files|*.xlsx";

            SalvarArquivo.DefaultExt="*.xlsx";
            SalvarArquivo.Title="Exportar resultado";
            var res = SalvarArquivo.ShowDialog();

            if(res==DialogResult.OK) {
                var ExcelArquivo = new XLWorkbook();


                var worksheet = ExcelArquivo.Worksheets.Add("Dados do Ensaio");

                worksheet.Cell("A1").Value="Tempo (s)";
                worksheet.Cell("B1").Value="Carga (gf)";
                worksheet.Cell("C1").Value="Posicao (mm)";

                int linha = 2;

                foreach(var row in dados) {
                    worksheet.Cell($"A{linha}").Value=row.Z;
                    worksheet.Cell($"B{linha}").Value=row.X;
                    worksheet.Cell($"C{linha}").Value=row.Y;

                    linha++;
                }

                if(Test.Tipo==TipoDeTeste.TPA) {
                    ResultadosTPA resTPA = ResultadosTPA.CalcTPA(corpoDeProva.Resultado,5);

                    worksheet.Cell("E1").Value="Altura(mm)";
                    worksheet.Cell("E2").Value=resTPA.TamProd;
                    worksheet.Cell("F1").Value="Dureza(gf)";
                    worksheet.Cell("F2").Value=resTPA.Hardness;
                    worksheet.Cell("G1").Value="Elasticidade(%)";
                    worksheet.Cell("G2").Value=resTPA.Springiness;
                    worksheet.Cell("G2").Style.NumberFormat.Format="0.00%";
                    worksheet.Cell("H1").Value="Coesividade(%)";
                    worksheet.Cell("H2").Value=resTPA.Cohesiveness;
                    worksheet.Cell("H2").Style.NumberFormat.Format="0.00%";
                    worksheet.Cell("I1").Value="Resiliência(%)";
                    worksheet.Cell("I2").Value=resTPA.Resilience;
                    worksheet.Cell("I2").Style.NumberFormat.Format="0.00%";
                    worksheet.Cell("J1").Value="Adesividade(gf.s)";
                    worksheet.Cell("J2").Value=resTPA.Adhesiveness;
                    worksheet.Cell("K1").Value="Gomosidade(gf)";
                    worksheet.Cell("K2").Value=resTPA.Gumminess;
                    worksheet.Cell("L1").Value="Mastigabilidade(gf)";
                    worksheet.Cell("L2").Value=resTPA.Chewiness;

                    worksheet.Cell("E4").Value="Area 1:2(gf.s)";
                    worksheet.Cell("E5").Value=resTPA.A12;
                    worksheet.Cell("F4").Value="Area 2:3(gf.s)";
                    worksheet.Cell("F5").Value=resTPA.A23;
                    worksheet.Cell("G4").Value="Area 1:3(gf.s)";
                    worksheet.Cell("G5").Value=resTPA.A13;
                    worksheet.Cell("H4").Value="Area 4:5(gf.s)";
                    worksheet.Cell("H5").Value=resTPA.A45;
                    worksheet.Cell("I4").Value="Area 5:6(gf.s)";
                    worksheet.Cell("I5").Value=resTPA.A56;
                    worksheet.Cell("J4").Value="Area 4:6(gf.s)";
                    worksheet.Cell("J5").Value=resTPA.A46;

                    worksheet.Cell("E7").Value="Tempo dif. 1:2(s)";
                    worksheet.Cell("E8").Value=resTPA.T1;
                    worksheet.Cell("F7").Value="Tempo dif. 4:5(s)";
                    worksheet.Cell("F8").Value=resTPA.T2;
                }
                ExcelArquivo.SaveAs(SalvarArquivo.FileName);
                return true;
            }
            return false;
        }
    }
#endif

    public class ExportacaoRelatorioPDF {
        public ExportacaoRelatorioPDF() { }


        public static bool exportaPDF(in CorpoDeProva corpoDeProva,in DataTest Teste,Image image) {
            Document doc = new Document(PageSize.A4);
            doc.SetMargins(45,45,64,45);
            
            doc.AddCreationDate();

            SaveFileDialog SalvarArquivo = new SaveFileDialog();
            SalvarArquivo.SupportMultiDottedExtensions=true;
            SalvarArquivo.Filter="PDF Files|*.pdf";
            SalvarArquivo.DefaultExt="*.pdf";
            SalvarArquivo.Title="Exportar resultados";
            SalvarArquivo.OverwritePrompt=true;

            var res = SalvarArquivo.ShowDialog();
            
            if(res==DialogResult.OK) {
                try {
                   
                    PdfWriter writer = PdfWriter.GetInstance(doc,new FileStream(SalvarArquivo.FileName,FileMode.Create));

                    doc.Open();

                    PdfPTable header = new PdfPTable(2);
                    header.TotalWidth=doc.PageSize.Width;
                    float[] w = { 74f,doc.PageSize.Width-74f };
                    header.SetWidths(w);
                    header.DefaultCell.Border=PdfPCell.NO_BORDER;
                    PdfPCell cabecalho = new PdfPCell();
                    cabecalho.Border=PdfPCell.NO_BORDER;

                    Image im = global::Texturometer.Properties.Resources.icon;
                    iTextSharp.text.Image Logo = iTextSharp.text.Image.GetInstance(im,new BaseColor(Color.White));
                    Logo.ScaleAbsolute(54,54);
                    Logo.Alignment=PdfPCell.ALIGN_CENTER;
                    PdfPCell cellImagem = new PdfPCell();
                    cellImagem.AddElement(Logo);
                    cellImagem.Border=PdfPCell.NO_BORDER;
                    cellImagem.Padding=0;
                    header.AddCell(cellImagem);


                    Paragraph ph = new Paragraph();
                    ph.Add("\nTITAN TEXTURE");
                    ph.Alignment=Element.ALIGN_CENTER;
                    ph.Font=FontFactory.GetFont("SansSerif",22,(int)FontStyle.Bold,new BaseColor(0x6B,0x4D,0x3E));
                    PdfPCell cellTexto = new PdfPCell();
                    cellTexto.VerticalAlignment=Element.ALIGN_CENTER;
                    cellTexto.AddElement(ph);
                    cellTexto.Border=PdfPCell.NO_BORDER;
                    cellTexto.HorizontalAlignment=Element.ALIGN_CENTER;

                    
                    header.AddCell(cellTexto);
                    header.WriteSelectedRows(0,-1,0,doc.PageSize.Height-doc.TopMargin+header.TotalHeight,writer.DirectContent);

                    im=global::Texturometer.Properties.Resources.MargemSup;
                    iTextSharp.text.Image Marca = iTextSharp.text.Image.GetInstance(im,new BaseColor(Color.White));
                    float scale = (doc.PageSize.Width)/Marca.Width;
                    Marca.ScaleAbsoluteWidth(scale*Marca.Width);
                    Marca.ScaleAbsoluteHeight(scale*Marca.Height);
                    Marca.Alignment=Element.ALIGN_CENTER;
                    doc.Add(Marca);


                    doc.Add(new Paragraph("\n",FontFactory.GetFont("Arial",9)));

                    Paragraph Titulo1 = new Paragraph();
                    Titulo1.SetLeading(0,1.2f);
                    Titulo1.Font=FontFactory.GetFont("Arial",12,(int)FontStyle.Bold);
                    Titulo1.Alignment=Element.ALIGN_LEFT;
                    Titulo1.IndentationRight=1;
                    Titulo1.Add(new Chunk("Informações de Ensaio:"));

                    doc.Add(Titulo1);

                    doc.Add(new Paragraph("\n",FontFactory.GetFont("Arial",9)));
                    
                    {
                        Paragraph InfsEnsaio = new Paragraph();
                        InfsEnsaio.SetLeading(0,1.2f);
                        InfsEnsaio.Alignment=Element.ALIGN_LEFT;
                        InfsEnsaio.IndentationRight=1;
                        InfsEnsaio.Add(new Chunk("Nome da Amostra: ",FontFactory.GetFont("Arial",9,(int)FontStyle.Bold)));
                        InfsEnsaio.Add(new Chunk(Teste.Nome,FontFactory.GetFont("Arial",9)));
                        InfsEnsaio.Add("\n");
                        InfsEnsaio.Add(new Chunk("Data e Hora: ",FontFactory.GetFont("Arial",9,(int)FontStyle.Bold)));
                        InfsEnsaio.Add(new Chunk(Teste.DataHora.ToString("yyyy-MM-dd HH:mm:ss"),FontFactory.GetFont("Arial",9)));
                        InfsEnsaio.Add("\n");
                        InfsEnsaio.Add(new Chunk("Tipo de Ensaio: ",FontFactory.GetFont("Arial",9,(int)FontStyle.Bold)));
                        InfsEnsaio.Add(new Chunk(Teste.Tipo.ToString(),FontFactory.GetFont("Arial",9)));

                        doc.Add(InfsEnsaio);
                    }

                    doc.Add(new Paragraph("\n",FontFactory.GetFont("Arial",9)));

                    Paragraph Titulo2 = new Paragraph();
                    Titulo2.SetLeading(0,1.2f);
                    Titulo2.Font=FontFactory.GetFont("Arial",12,(int)FontStyle.Bold);
                    Titulo2.Alignment=Element.ALIGN_LEFT;
                    Titulo2.IndentationRight=1;
                    Titulo2.Add(new Chunk("Parâmetros:"));

                    doc.Add(new Paragraph("\n",FontFactory.GetFont("Arial",9)));

                    doc.Add(Titulo2);


                    {
                        PdfPTable infs = new PdfPTable(2);
                        infs.SpacingBefore=9;
                        infs.WidthPercentage=100;
                        PdfPCell infsLeft = new PdfPCell();
                        infsLeft.Border =PdfPCell.NO_BORDER;
                        PdfPCell infsRight = new PdfPCell();
                        infsRight.Border=PdfPCell.NO_BORDER;


                        {
                            Paragraph pars1 = new Paragraph();
                            pars1.SetLeading(0,1.2f);
                            pars1.Alignment=Element.ALIGN_LEFT;
                            pars1.IndentationRight=1;
                            pars1.Add(new Chunk("Velocidade pré-teste: ",FontFactory.GetFont("Arial",9,(int)FontStyle.Bold)));
                            pars1.Add(new Chunk(Teste.VelPreTeste.ToString()+" mm/s",FontFactory.GetFont("Arial",9)));
                            pars1.Add("\n");
                            pars1.Add(new Chunk("Velocidade de Teste: ",FontFactory.GetFont("Arial",9,(int)FontStyle.Bold)));
                            pars1.Add(new Chunk(Teste.VelTeste.ToString()+" mm/s",FontFactory.GetFont("Arial",9)));
                            pars1.Add("\n");
                            pars1.Add(new Chunk("Velocidade pós-teste: ",FontFactory.GetFont("Arial",9,(int)FontStyle.Bold)));
                            pars1.Add(new Chunk(Teste.VelPosTeste.ToString()+"m m/s",FontFactory.GetFont("Arial",9)));
                            pars1.Add("\n");
                            pars1.Add(new Chunk("Tipo de Alvo: ",FontFactory.GetFont("Arial",9,(int)FontStyle.Bold)));
                            pars1.Add(new Chunk(Teste.TipoLimite.ToString(),FontFactory.GetFont("Arial",9)));
                            pars1.Add("\n");
                            var un=Teste.TipoLimite==TipoTarget.Distancia ? "mm" : Teste.TipoLimite==TipoTarget.Deformacao ? "%" : "gf";
                            var valAlvo = Teste.TipoLimite==TipoTarget.Deformacao ? Teste.ValorLimite*100 : Teste.ValorLimite;

                            pars1.Add(new Chunk("Valor Alvo: ",FontFactory.GetFont("Arial",9,(int)FontStyle.Bold)));
                            pars1.Add(new Chunk(valAlvo.ToString()+ " "+un,FontFactory.GetFont("Arial",9)));
                            pars1.Add("\n");

                            pars1.Add(new Chunk("Tempo de Intervalo: ",FontFactory.GetFont("Arial",9,(int)FontStyle.Bold)));
                            pars1.Add(new Chunk(Teste.Tempo.ToString() + " s",FontFactory.GetFont("Arial",9)));
                            infsLeft.AddElement(pars1);

                        }

                        {
                            Paragraph pars2 = new Paragraph();
                            pars2.SetLeading(0,1.2f);
                            pars2.Alignment=Element.ALIGN_LEFT;
                            pars2.IndentationRight=1;
                            pars2.Add(new Chunk("Tipo de Detecção: ",FontFactory.GetFont("Arial",9,(int)FontStyle.Bold)));
                            pars2.Add(new Chunk(Teste.TipoDeteccao.ToString(),FontFactory.GetFont("Arial",9)));
                            pars2.Add("\n");
                             var un=Teste.TipoDeteccao==TipoTrigger.Distancia ? "mm" : Teste.TipoDeteccao==TipoTrigger.Forca ? "gf" : String.Empty;

                            pars2.Add(new Chunk("Valor de Detecção: ",FontFactory.GetFont("Arial",9,(int)FontStyle.Bold)));
                            pars2.Add(new Chunk(Teste.ValorDeteccao.ToString()+ " "+un,FontFactory.GetFont("Arial",9)));
                            pars2.Add("\n");

                            pars2.Add(new Chunk("Tipo de Tara: ",FontFactory.GetFont("Arial",9,(int)FontStyle.Bold)));
                            pars2.Add(new Chunk(Teste.TipoTara.ToString(),FontFactory.GetFont("Arial",9)));
                            pars2.Add("\n");

                            pars2.Add(new Chunk("Tipo de Probe: ",FontFactory.GetFont("Arial",9,(int)FontStyle.Bold)));
                            pars2.Add(new Chunk(Teste.PontaDeTeste.Tipo.ToString(),FontFactory.GetFont("Arial",9)));

                            pars2.Add("\n");
                            pars2.Add(new Chunk("Dimensões da Probe: ",FontFactory.GetFont("Arial",9,(int)FontStyle.Bold)));
                            pars2.Add(new Chunk(Teste.PontaDeTeste.getDimin(),FontFactory.GetFont("Arial",9)));

                            infsRight.AddElement(pars2);
                        }


                        infs.AddCell(infsLeft);
                        infs.AddCell(infsRight);

                        doc.Add(infs);
                    }

                    Paragraph Titulo3= new Paragraph();
                    Titulo3.SetLeading(0,1.2f);
                    Titulo3.Font=FontFactory.GetFont("Arial",12,(int)FontStyle.Bold);
                    Titulo3.Alignment=Element.ALIGN_LEFT;
                    Titulo3.IndentationRight=1;
                    Titulo3.Add(new Chunk("Gráfico:"));

                    doc.Add(new Paragraph("\n",FontFactory.GetFont("Arial",9)));

                    doc.Add(Titulo3);

                    iTextSharp.text.Image Graph = iTextSharp.text.Image.GetInstance(image,new BaseColor(Color.White));
                    scale = (doc.PageSize.Width-doc.RightMargin-doc.LeftMargin)/Graph.Width;
                    Graph.ScaleAbsoluteWidth(scale*Graph.Width);
                    Graph.ScaleAbsoluteHeight(scale*Graph.Height);
                    Graph.Alignment=Element.ALIGN_CENTER;
                    doc.Add(Graph);

                    doc.Add(new Paragraph("\n",FontFactory.GetFont("Arial",9)));

                    Paragraph Titulo4 = new Paragraph();
                    Titulo4.SetLeading(0,1.2f);
                    Titulo4.Font=FontFactory.GetFont("Arial",12,(int)FontStyle.Bold);
                    Titulo4.Alignment=Element.ALIGN_LEFT;
                    Titulo4.IndentationRight=1;
                    Titulo4.Add(new Chunk("Resultados:"));
                    
                    {
                        var tb = corpoDeProva.Resultado;

                        if(Teste.Tipo==TipoDeTeste.TPA) {
                            ResultadosTPA resTPA = ResultadosTPA.CalcTPA(tb,5);


                            PdfPTable infs = new PdfPTable(2);
                            infs.SpacingBefore=9;
                            infs.WidthPercentage=100;
                            PdfPCell infsLeft = new PdfPCell();
                            infsLeft.Border=PdfPCell.NO_BORDER;
                            PdfPCell infsRight = new PdfPCell();
                            infsRight.Border=PdfPCell.NO_BORDER;


                            {
                                Paragraph pars1 = new Paragraph();
                                pars1.SetLeading(0,1.2f);
                                pars1.Alignment=Element.ALIGN_LEFT;
                                pars1.IndentationRight=1;
                                pars1.Add(new Chunk("Tamanho do produto: ",FontFactory.GetFont("Arial",9,(int)FontStyle.Bold)));
                                pars1.Add(new Chunk(resTPA.TamProd.ToString()+ " mm",FontFactory.GetFont("Arial",9)));
                                pars1.Add("\n");
                                pars1.Add(new Chunk("Dureza: ",FontFactory.GetFont("Arial",9,(int)FontStyle.Bold)));
                                pars1.Add(new Chunk(Math.Round(resTPA.Hardness,2).ToString()+ " gf",FontFactory.GetFont("Arial",9)));
                                pars1.Add("\n");
                                pars1.Add(new Chunk("Elasticidade: ",FontFactory.GetFont("Arial",9,(int)FontStyle.Bold)));
                                pars1.Add(new Chunk(Math.Round(resTPA.Springiness*100,2).ToString() +" %",FontFactory.GetFont("Arial",9)));
                                pars1.Add("\n");

                                pars1.Add(new Chunk("Coesividade: ",FontFactory.GetFont("Arial",9,(int)FontStyle.Bold)));
                                pars1.Add(new Chunk(Math.Round(resTPA.Cohesiveness*100,2).ToString()+" %",FontFactory.GetFont("Arial",9)));
                                pars1.Add("\n");

                                infsLeft.AddElement(pars1);

                            }

                            {
                                Paragraph pars2 = new Paragraph();
                                pars2.SetLeading(0,1.2f);
                                pars2.Alignment=Element.ALIGN_LEFT;
                                pars2.IndentationRight=1;
                                pars2.Add(new Chunk("Resiliência: ",FontFactory.GetFont("Arial",9,(int)FontStyle.Bold)));
                                pars2.Add(new Chunk(Math.Round(resTPA.Resilience*100,2).ToString() + " %",FontFactory.GetFont("Arial",9)));
                                pars2.Add("\n");

                                pars2.Add(new Chunk("Adesividade: ",FontFactory.GetFont("Arial",9,(int)FontStyle.Bold)));
                                pars2.Add(new Chunk(Math.Round(resTPA.Adhesiveness,2).ToString() +" gf.s",FontFactory.GetFont("Arial",9)));
                                pars2.Add("\n");

                                pars2.Add(new Chunk("Gomosidade: ",FontFactory.GetFont("Arial",9,(int)FontStyle.Bold)));
                                pars2.Add(new Chunk(Math.Round(resTPA.Gumminess,2).ToString()+" gf",FontFactory.GetFont("Arial",9)));
                                pars2.Add("\n");

                                pars2.Add(new Chunk("Mastigabilidade: ",FontFactory.GetFont("Arial",9,(int)FontStyle.Bold)));
                                pars2.Add(new Chunk(Math.Round(resTPA.Chewiness,2).ToString()+" gf",FontFactory.GetFont("Arial",9)));


                                infsRight.AddElement(pars2);
                            }


                            infs.AddCell(infsLeft);
                            infs.AddCell(infsRight);
                            


                            doc.Add(infs);
                        }

                    }

                    PdfPTable baseboard = new PdfPTable(1);
                    baseboard.TotalWidth=doc.PageSize.Width;
                    baseboard.DefaultCell.Border=PdfPCell.NO_BORDER;


                    im = global::Texturometer.Properties.Resources.MargemInf;
                    iTextSharp.text.Image imRodape = iTextSharp.text.Image.GetInstance(im,new BaseColor(Color.White));
                    scale=(doc.PageSize.Width)/imRodape.Width;
                    imRodape.ScaleAbsoluteWidth(scale*imRodape.Width);
                    imRodape.ScaleAbsoluteHeight(scale*imRodape.Height);
                    imRodape.Alignment=PdfPCell.ALIGN_CENTER;
                    PdfPCell cellImMargInf = new PdfPCell();
                    cellImMargInf.Border=PdfPCell.NO_BORDER;
                    cellImMargInf.Padding=0;
                    cellImMargInf.AddElement(imRodape);
                    baseboard.AddCell(cellImMargInf);


                    Paragraph phRp = new Paragraph(@"TCC - BH3MO - 2024");
                    phRp.Alignment=Element.ALIGN_RIGHT;
                    phRp.Font=FontFactory.GetFont("SansSerif",8,(int)FontStyle.Bold,new BaseColor(0x6B,0x4D,0x3E));
                    phRp.SpacingBefore=0;
                    phRp.SpacingAfter=0;
                    phRp.IndentationRight=12;
                    PdfPCell cellTxRodape = new PdfPCell();
                    cellTxRodape.HorizontalAlignment=Element.ALIGN_TOP;
                    cellTxRodape.RightIndent=12;
                    cellTxRodape.Border=PdfPCell.NO_BORDER;
                    cellTxRodape.HorizontalAlignment=Element.ALIGN_CENTER;
                    cellTexto.PaddingRight=12;
                    cellTxRodape.AddElement(phRp);
                    baseboard.AddCell(cellTxRodape);
                    baseboard.WriteSelectedRows(0,-1,0,10+baseboard.TotalHeight,writer.DirectContent);


                    doc.Close();
                }catch(Exception ex){
                    MessageBox.Show(ex.Message,"Erro ao salvar!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                return true;
            }
            return false;
        
        }
    }

    public class ExportacaoCSV {
        public ExportacaoCSV() { 
        
        }

        public static bool exportarCSV(in CorpoDeProva corpoDeProva) {

            var dados = corpoDeProva.Resultado.GetTable();

            var config = new CsvConfiguration(CultureInfo.CurrentCulture);
            config.HasHeaderRecord=true;

            SaveFileDialog SalvarArquivo = new SaveFileDialog();
            SalvarArquivo.Filter="CSV Files|*.csv";
            SalvarArquivo.DefaultExt="*.csv";
            SalvarArquivo.Title="Exportar resultados";

            var res = SalvarArquivo.ShowDialog();
            string dl;

            if(res==DialogResult.OK) {
                using(var writer = new StreamWriter(SalvarArquivo.FileName,false,Encoding.Default))
                using(var csv = new CsvWriter(writer,config)) {

                    // Escreve o cabeçalho
                    csv.WriteField("Tempo (s)");
                    csv.WriteField("Carga (gf)");
                    csv.WriteField("Posicao (mm)");
                    csv.NextRecord();

                    // Escreve os dados
                    foreach(var dado in dados) {
                        csv.WriteField(dado.Z);
                        csv.WriteField(dado.X);
                        csv.WriteField(dado.Y);

                        csv.NextRecord();
                    }
                    dl=csv.Configuration.Delimiter;
                }
                return true;
            }
            return false;

        }
    }
}
