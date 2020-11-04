## Linq2db LoadWith performance regression test

This repo contains sample projects with the performance problem detected in Linq2Db with LoadWith extension method.

The projects are exactly equal except that one references linq2db@2.9.8 and the other one referneces linq2db@3.1.6.

The project with version 2.9.8 generates the SQL in 1 second while the project with version 3.1.6 timeouts without generating the SQL.

---

I want to thank you linq2db mantainers for the great tools that are given us developers.