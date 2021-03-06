﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.Services
{
    public interface IOpenFileDialogService
    {
        string ShowSaveFileDialog();
        string ShowSaveFileDialog(string defaultext, string filter);
        string ShowOpenFileDialog();
    }
}
