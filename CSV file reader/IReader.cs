﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_file_reader
{
    interface IReader
    {
        IEnumerable<RowData> Rows(IEnumerable<IRowData> GetRows);
    }
}
