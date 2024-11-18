create database ToDo
use ToDO

create table Tasks
(
task_id int primary key identity(1,1),
task_name nvarchar(20),
task_begin datetime,
task_deadline datetime
)

