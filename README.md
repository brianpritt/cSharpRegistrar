## Product Spec

| Description                                                                   | Input                         | Output                        |
|-------------------------------------------------------------------------------|-------------------------------|-------------------------------|
| Program will be able to add a new course to the database                      | Cultural Anthropology         | 1                             |
| Program will be able to retrieve the course from the database                 | Cultural Anthropology         | Cultural Anthropology         |
| Program will be able to retrieve all courses in the database                  | {2 courses}                   | {2 courses}                   |
| Program will be able to add a new student to the database                     | Brian                         | 1                             |
| Program will be able to retrieve the student from the database                | Brian                         | Brian                         |
| Program will be able to retrieve all students in the database                 | {2 students}                  | {2 students}                  |
| Program can assign a student to a course                                      | Brian : Cultural Anthropology | 1                             |
| Program should be able to retrieve all students enrolled in a specific course | Brian : Cultural Anthropology | Brian : Cultural Anthropology |
| Program should be able to retrieve all course a specific student is enrolled  | Cultural Anthropology : Brian | Cultural Anthropology : Brian |
