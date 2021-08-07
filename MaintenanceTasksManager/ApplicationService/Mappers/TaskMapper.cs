namespace ApplicationService.Mappers
{
    using System.Collections.Generic;
    using System.Linq;
    using ApplicationService.Models.Tasks;

    public class TaskMapper
    {
        public static IEnumerable<MaintenanceTask> FromDto(IEnumerable<DataAccess.Models.MaintenanceTask> dtos)
        {
            return dtos.Where(dto => dto != null).Select(FromDto);
        }

        public static MaintenanceTask FromDto(DataAccess.Models.MaintenanceTask dto)
        {
            return dto == null
                ? default
                : new MaintenanceTask
                {
                    Id = dto.Id,
                    Date = dto.Date,
                    Owner = dto.Owner,
                    Summary = dto.Summary
                };
        }

        public static DataAccess.Models.MaintenanceTask ToDto(MaintenanceTask task)
        {
            return task == null
                ? default
                : new DataAccess.Models.MaintenanceTask
                {
                    Id = task.Id,
                    Date = task.Date,
                    Owner = task.Owner,
                    Summary = task.Summary
                };
        }
    }
}