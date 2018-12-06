using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.Services
{
    public class OpenFileDialogService : IOpenFileDialogService
    {
        
        public string ShowOpenFileDialog()
        {
            string result="";
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|All Files (*.*)|*.*";


            // Display OpenFileDialog by calling ShowDialog method 
            bool? res = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (res == true)
            {
                // Open document 
                result = dlg.FileName;
                //MessageBox.Show("Observation image selected: " + ObservationImgPath);
                

            }
            return result;
        }

        
        public string ShowSaveFileDialog()
        {
            // Create OpenFileDialog 
            // Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();



            // Set filter for file extension and default file extension 
            sfd.DefaultExt = ".docx";
            sfd.Filter = "Docx Files (*.docx)| All files (*.*)";


            // Display OpenFileDialog by calling ShowDialog method 
            //Nullable<bool> result = dlg.ShowDialog();

            Nullable<bool> result2 = sfd.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result2 == true)
            {
                // Open document 
                return sfd.FileName;

            }
            else
            {
                return null;
            }
        }

        public string ShowSaveFileDialog(string defaultext, string filter)
        {
            // Create OpenFileDialog 
            // Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();



            // Set filter for file extension and default file extension 
            //sfd.DefaultExt = ".docx";
            //sfd.Filter = "Docx Files (*.docx)| All files (*.*)";

            sfd.DefaultExt = defaultext;
            sfd.Filter = filter;


            // Display OpenFileDialog by calling ShowDialog method 
            //Nullable<bool> result = dlg.ShowDialog();

            Nullable<bool> result2 = sfd.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result2 == true)
            {
                // Open document 
                return sfd.FileName;

            }
            else
            {
                return null;
            }
        }
    }
}
