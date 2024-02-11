using IMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Service.Interface;

public interface IWarehouseService
{
    List<Warehouse> GetAllWarehouses();
    Warehouse GetWarehouseById(int? id); 
    void CreateNewWarehouse(Warehouse newWarehouse);
    void UpdateWarehouse(int id, Warehouse newWarehouse);
    void DeleteWarehouse(int id);   
}
