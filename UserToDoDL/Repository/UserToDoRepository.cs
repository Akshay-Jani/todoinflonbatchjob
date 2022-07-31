using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using UserToDoDL.Model;

namespace UserToDoDL.Repository
{
    public class UserToDoRepository
    {
        public static async Task<int> SyncDataWithDb(List<ToDo> toDos)
        {
            List<ToDoTableValue> toDoTableValues = new List<ToDoTableValue>();
            if (toDos.Any())
            {
                toDos.ForEach(x =>
                {
                    toDoTableValues.Add(new ToDoTableValue()
                    {
                        UserId = x.UserId,
                        Title = x.Title,
                        Completed = x.Completed
                    });
                });
            }

            DataTable toDoTable = DbHelper.GetDataTable<ToDoTableValue>(toDoTableValues);
            return await DbHelper.PostAsync<int>("usp_AddToDoList", new {
                @toDoList = toDoTable
            });
        }
    }
}
