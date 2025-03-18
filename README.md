Secret Santa Web Application 🎅🎁
This is a .NET Core MVC web application for managing a Secret Santa event. Employees upload a CSV file, and the system automatically assigns a secret child to each participant while avoiding previous year assignments.

Features 🚀
✅ Upload a CSV file containing employee details.
✅ Upload a CSV file containing previous year’s assignments (to prevent duplicates).
✅ Generate Secret Santa assignments while following rules.
✅ Download the generated Secret Santa assignments as a CSV file.
✅ Simple and user-friendly web interface.
✅ Robust error handling.

Technologies Used 🛠
ASP.NET Core MVC 6
C#
HTML, CSS, Bootstrap
File Handling for CSV processing
Installation & Setup 💻

1. Clone the Repository
bash
Copy
Edit
[git clone https://github.com/yourusername/SecretSantaMVC.git](https://github.com/albinjoy931/SecretSantaMVC)
cd SecretSantaMVC 

2. Open in Visual Studio
Open SecretSantaMVC.sln in Visual Studio 2022 or later.


3. Restore Dependencies
bash
Copy
Edit
dotnet restore


4. Run the Application
bash
Copy
Edit
dotnet run
The application will start at:
http://localhost:5000 (or another port assigned).

*****************************
How to Use 🎄
*******************************
*************************

Step 1: Upload Employee CSV
Click "Upload Employee File".

Select a CSV file with the following format:

graphql
Copy
Edit
Employee_Name,Employee_EmailID
John Doe,john@example.com
Jane Doe,jane@example.com
*********************************

Step 2: Upload Previous Assignments (Optional)
Click "Upload Previous Assignments".

Select a CSV file to avoid assigning the same secret child as last year:

graphql
Copy
Edit
Employee_Name,Employee_EmailID,Secret_Child_Name,Secret_Child_EmailID
John Doe,john@example.com,Jane Doe,jane@example.com
***************************************************************

Step 3: Generate Assignments
Click "Generate Secret Santa Assignments".
The system assigns each employee a unique secret child.

********************************************************
Step 4: Download Assignments
Click "Download CSV" to get the final assignments.

CSV format:

graphql
Copy
Edit
Employee_Name,Employee_EmailID,Secret_Child_Name,Secret_Child_EmailID
John Doe,john@example.com,Alice Smith,alice@example.com
