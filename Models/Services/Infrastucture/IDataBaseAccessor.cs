using System;
using System.Data;
using System.Threading.Tasks;

namespace MyCourse.Models.Services.Infrastucture
{
    public interface IDataBaseAccessor
    {
        Task<DataSet> QueryAsync(FormattableString query);
    }
}