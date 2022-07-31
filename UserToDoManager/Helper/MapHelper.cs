using System.Collections.Generic;
using System.Linq;
using UserToDoBL.Model;
using UserToDoBL.ViewModel;

namespace UserToDoBL.Helper
{
    public class MapHelper
    {
        public static ToDo MapToDoViewModelToModel(ToDoViewModel toDoViewModel)
        {
            ToDo toDo = new ToDo()
            {
                UserId = toDoViewModel.UserId,
                Id = toDoViewModel.Id,
                Title = toDoViewModel.Title,
                Completed = toDoViewModel.Completed
            };
            return toDo;
        }

        public static List<ToDo> MapToDoListToModel(ToDoList toDoList)
        {
            List<ToDo> toDos = new List<ToDo>();
            if (toDoList.ToDosList.Any())
            {
                toDoList.ToDosList.ForEach(x =>
                {
                    toDos.Add(MapToDoViewModelToModel(x));
                });
            }
            return toDos;
        }

        public static List<UserToDoDL.Model.ToDo> MapToBLToDL(List<ToDo> toDos)
        {
            List<UserToDoDL.Model.ToDo> toDoDLs = new List<UserToDoDL.Model.ToDo>();
            if (toDos.Any())
            {
                toDos.ForEach(x =>
                {
                    toDoDLs.Add(new UserToDoDL.Model.ToDo()
                    {
                        UserId = x.UserId,
                        Id = x.Id,
                        Title = x.Title,
                        Completed = x.Completed
                    });
                });
            }
            return toDoDLs;
        }
    }
}
