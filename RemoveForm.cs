using System;
using System.IO;
using System.Windows.Forms;

namespace RemoveRevitAddin
{
    public partial class RemoveForm : Form
    {
        public RemoveForm()
        {
            InitializeComponent(); CenterToParent(); // 置中
        }
        // 確定
        private void sureBtn_Click(object sender, EventArgs e)
        {
            foreach (var versionItem in checkedListBox1.CheckedItems)
            {
                foreach (var productItem in checkedListBox2.CheckedItems)
                {
                    string fileName = string.Empty;
                    if (productItem.ToString().Equals("中興自動化"))
                    {
                        fileName = "Sinotech";
                    }
                    else if (productItem.ToString().Equals("捷運規範校核"))
                    {
                        fileName = "Sino_Station";
                    }
                    string directoryPath = @"C:\ProgramData\Autodesk\Revit\Addins\" + versionItem.ToString() + @"\" + fileName; // 資料夾
                    string addinPath = @"C:\ProgramData\Autodesk\Revit\Addins\" + versionItem.ToString() + @"\" + fileName + ".addin"; // addin
                    // 檢查是否有此addin, 有的話則移除
                    bool fileExists = File.Exists(addinPath);
                    if (fileExists == true)
                    {
                        try
                        {
                            File.Delete(addinPath);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message + "\n" + ex.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine("找不到檔案。");
                    }
                    // 檢查是否有此資料夾, 有的話則移除
                    bool directoryExists = Directory.Exists(directoryPath);
                    if (directoryExists == true)
                    {
                        DeleteFolder(directoryPath);
                    }
                    else
                    {
                        Console.WriteLine("無此資料夾。");
                    }
                }
            }
            Close(); // 移除後關閉
        }
        // 完整刪除資料夾
        public static void DeleteFolder(string dir)
        {
            try
            {
                foreach (string d in Directory.GetFileSystemEntries(dir))
                {
                    try
                    {
                        if (File.Exists(d))
                        {
                            // 如果是唯獨則先修改
                            FileInfo fi = new FileInfo(d);
                            if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                            {
                                fi.Attributes = FileAttributes.Normal;
                            }
                            File.Delete(d); // 直接删除其中的文件
                        }
                        else
                        {
                            DeleteFolder(d); // 遞迴刪除子資料夾
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message + "\n" + ex.ToString());
                    }
                }
                Directory.Delete(dir); // 刪除已清空的資料夾
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.ToString());
            }
        }
        // 取消
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close(); // 關閉
        }
    }
}