using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IExternalServices
{
    interface IService
    {
        Task<object> Get(int id);
        IEnumerable<object> GetAll();
        IEnumerable<object> GetAllBySearch(object searchViewModel);
        Task Add(object model);
        void Update(object model);
        bool Delete(int id);
    }
}
