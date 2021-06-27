RESTORE DATABASE ProfileSampleDB
FROM DISK = 'C:\Users\eugen\Desktop\Mentoring\.Net-Advanced-Mentoring\Module 5 - Profiling\ProfileSample\ProfileSample\App_Data\sample.bak'

WITH MOVE 'ProfileSample' TO 'C:\Users\eugen\Desktop\Mentoring\.Net-Advanced-Mentoring\Module 5 - Profiling\ProfileSample\ProfileSample\App_Data\ProfileSample.mdf',
MOVE 'ProfileSample_log' TO 'C:\Users\eugen\Desktop\Mentoring\.Net-Advanced-Mentoring\Module 5 - Profiling\ProfileSample\ProfileSample\App_Data\ProfileSample.ldf',
REPLACE;