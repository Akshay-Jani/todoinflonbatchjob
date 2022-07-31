using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserToDoBL.Helper;
using UserToDoBL.Model;
using UserToDoBL.ViewModel;
using UserToDoDL.Repository;

namespace UserToDoBL.Service
{
    public static class UserToDoService
    {
        public static async Task PostToDoList(ToDoList toDoList)
        {
            if (toDoList != null && toDoList.ToDosList.Any())
            {
                List<ToDo> toDos = MapHelper.MapToDoListToModel(toDoList);
                List<UserToDoDL.Model.ToDo> toDoDLs = MapHelper.MapToBLToDL(toDos);
                await UserToDoRepository.SyncDataWithDb(toDoDLs);
            }
        }
    }
}
