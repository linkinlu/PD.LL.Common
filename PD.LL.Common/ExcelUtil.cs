using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ganss.Excel;
using NPOI.HSSF.UserModel;
using Npoi.Mapper;
using NPOI.XSSF.UserModel;


public class ExcelUtil
{
    public async Task<List<T>> Read<T>(string file)
    {
        var res = await new ExcelMapper(file).FetchAsync<T>(file);
        return res?.ToList();

    }
}
