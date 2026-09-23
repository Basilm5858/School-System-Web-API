SCHOOL SYSTEM FRONTEND V5
=========================

This is the updated version after your latest DTO changes.

Latest DTO changes supported
----------------------------
TeacherDTO now includes:
- PhoneNumber
- Salary
- DepartmentId

SubjectDTO now includes:
- MaxGrade

What this fixes
---------------
Teacher Edit:
- FirstName / LastName are recovered from FullName.
- Email is pre-filled.
- PhoneNumber is pre-filled.
- Salary is pre-filled.
- Department dropdown is pre-selected using DepartmentId.
- No value needs to be typed again just to preserve it.

Subject Edit:
- Name is pre-filled.
- MaxGrade is now pre-filled.
- Description is pre-filled.
- Teacher dropdown is resolved from TeacherName.

Students:
- Classroom dropdown is available in Create and Edit, because
  CreateStudentDTO / UpdateStudentDTO contain ClassRoomId.

Subjects:
- Teacher dropdown is available in Create and Edit, because
  CreateSubjectDTO / UpdateSubjectDTO contain TeacherId.

Run
---
1. Start your ASP.NET Core API.
2. In app.js, confirm the API address:
   const API_BASE = "http://localhost:5203/api";
3. Open index.html with Live Server.

If CORS is needed
-----------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

Then:
app.UseCors("Frontend");

Main modules
------------
- Dashboard
- Departments
- Classrooms
- Teachers
- Students
- Subjects
- Enrollments

Each module includes:
- List
- Search
- View details
- Create
- Edit
- Delete

The Department module also includes search by teacher full name.
