﻿using GBMO.Teach.Core.Enums;

namespace GBMO.Teach.Core.DTOs.Output.Teacher.TeacherSchedule
{
    public class TeacherScheduleOutput
    {
        public DateTime ClassStartDate { get; set; }
        public DateTime ClassEndDate { get; set; }
        public ClassStatusses ClassStatusses { get; set; }
    }
}
