School System Frontend
======================

Files
-----
index.html
styles.css
app.js

Run
---
1. Start the ASP.NET Core Web API.
2. In app.js, set:
   const API_BASE = "http://localhost:5203/api";
   to the actual API URL/port.
3. Open this folder in VS Code.
4. Run index.html with Live Server.

CORS
----
If the browser reports a CORS error, enable CORS in Program.cs:

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

Then after:
var app = builder.Build();

add:
app.UseCors("Frontend");

Backend issues worth fixing
---------------------------
1. EnrollmentController.GetById:
   You fetch ONE Enrollment but map it to List<EnrollmentDTO>.
   Change:
       _mapper.Map<List<EnrollmentDTO>>(enrollments)
   to:
       _mapper.Map<EnrollmentDTO>(enrollments)
   and add a null check.

2. StudentController.UpdateStudent:
   It creates a new Student with AutoMapper and calls SaveChangesAsync,
   but that new object is not loaded/tracked as the existing database row.
   Find the existing Student first, then map the DTO onto it.

3. SubjectController.UpdateSubject:
   Same issue as Student update. Find the existing Subject first, then
   _mapper.Map(dto, subject), then SaveChangesAsync().

4. Student/Subject/Enrollment form field names:
   The controllers were provided, but their DTO class definitions were not.
   The frontend has reasonable fallback fields in the `resources` object at
   the top of app.js. If your actual DTO property names differ, edit only
   those schema entries; the rest of the UI does not need to change.

Current routes used by the frontend
-----------------------------------
Department:
GET    /api/Department
GET    /api/Department/{id}
GET    /api/Department/Search?fullName=...
POST   /api/Department
PUT    /api/Department/{id}
DELETE /api/Department?id=...

ClassRooms:
GET    /api/ClassRooms
GET    /api/ClassRooms/{id}
POST   /api/ClassRooms
PUT    /api/ClassRooms/{id}
DELETE /api/ClassRooms?id=...

Teacher:
GET    /api/Teacher
GET    /api/Teacher/{id}
POST   /api/Teacher
PUT    /api/Teacher?id=...
DELETE /api/Teacher?id=...

Student:
GET    /api/Student
GET    /api/Student/{id}
POST   /api/Student
PUT    /api/Student
DELETE /api/Student?id=...

Subject:
GET    /api/Subject
GET    /api/Subject/{id}
POST   /api/Subject
PUT    /api/Subject
DELETE /api/Subject?id=...

Enrollment:
GET    /api/Enrollment
GET    /api/Enrollment/{id}
POST   /api/Enrollment
PUT    /api/Enrollment?id=...
DELETE /api/Enrollment?id=...
