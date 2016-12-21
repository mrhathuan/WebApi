using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;

namespace Business
{
public interface IBLData<T> : IBase
{
DTOResult List(Kendo.Mvc.UI.DataSourceRequest request);
T Get(int id);
int Save(T item);
List<int> SaveList(List<T> lst);
void Delete(T item);
void DeleteList(List<T> lst);
}
}

