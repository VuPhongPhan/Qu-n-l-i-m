using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace Project_CuoiKi
{
    class Excel
    {
        public void Export(DataTable dt, string sheetName, string title)
        {
            SaveFileDialog dlgSave;

            //Tạo các đối tượng Excel

            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Workbooks oBooks;

            Microsoft.Office.Interop.Excel.Sheets oSheets;

            Microsoft.Office.Interop.Excel.Workbook oBook;

            Microsoft.Office.Interop.Excel.Worksheet oSheet;

            //Tạo mới một Excel WorkBook 

            oExcel.Visible = true;

            oExcel.DisplayAlerts = false;

            oExcel.Application.SheetsInNewWorkbook = 1;

            oBooks = oExcel.Workbooks;

            oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));

            oSheets = oBook.Worksheets;

            oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);

            oSheet.Name = sheetName;

            // Tạo phần đầu nếu muốn

            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "D1");

            head.MergeCells = true;

            head.Value2 = title;

            head.Font.Bold = true;

            head.Font.Name = "Tahoma";

            head.Font.Size = "18";

            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tạo tiêu đề cột 

              Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");

              cl1.Value2 = "Mã Môn";

              cl1.ColumnWidth = 15.0;

              Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");

              cl2.Value2 = "Tên Môn";

              cl2.ColumnWidth = 20.0;

              Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");

              cl3.Value2 = "Số tín chỉ";

              cl3.ColumnWidth = 10.0;

              Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");

              cl4.Value2 = "Điểm thi";

              cl4.ColumnWidth = 15.0;

              

              Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "D3");

              rowHead.Font.Bold = true;

              // Kẻ viền

              rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

              // Thiết lập màu nền

              rowHead.Interior.ColorIndex = 15;

              rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tạo mẳng đối tượng để lưu dữ toàn bồ dữ liệu trong DataTable,

            // vì dữ liệu được được gán vào các Cell trong Excel phải thông qua object thuần.

            object[,] arr = new object[dt.Rows.Count, dt.Columns.Count];

            //Chuyển dữ liệu từ DataTable vào mảng đối tượng

            for (int r = 0; r < dt.Rows.Count; r++)

            {

                DataRow dr = dt.Rows[r];

                for (int c = 0; c < dt.Columns.Count; c++)

                {
                    arr[r, c] = dr[c];
                }
            }

            //Thiết lập vùng điền dữ liệu

            int rowStart = 4;

            int columnStart = 1;

            int rowEnd = rowStart + dt.Rows.Count - 1;

            int columnEnd = dt.Columns.Count;

            // Ô bắt đầu điền dữ liệu

            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, columnStart];

            // Ô kết thúc điền dữ liệu

            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnEnd];

            // Lấy về vùng điền dữ liệu

            Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range(c1, c2);

            //Điền dữ liệu vào vùng đã thiết lập

            range.Value2 = arr;

            // Kẻ viền

            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

            // Căn giữa cột STT

            Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnStart];

            Microsoft.Office.Interop.Excel.Range c4 = oSheet.get_Range(c1, c3);

            oSheet.get_Range(c3, c4).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            oSheet.Name = "KHang";
            oBook.Activate(); //Kích hoạt file Excel
                              //Thiết lập các thuộc tính của SaveFileDialog
            dlgSave = new SaveFileDialog();
            dlgSave.Filter = "Excel Document(*.xls)|*.xls  |Word Document(*.doc) |*.doc|All files(*.*)|*.*";
            dlgSave.FilterIndex = 1;
            dlgSave.AddExtension = true;
            dlgSave.DefaultExt = ".xls";
            if (dlgSave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                oBook.SaveAs(dlgSave.FileName.ToString());//Lưu file Excel
            oExcel.Quit();//Thoát khỏi ứng dụng
        }
    }
}
