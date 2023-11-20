# Project Title: Dewey Decimal: Finding Call Numbers (FINAL POE)

## Project Description

A head librarian is in need of a fun software application to teach other librarians how to use the Dewey Decimal System. The head librarian now wants to teach the other librarians using a match the column type of game.

## Technologies Used

- Visual Studio 2022
- C#
- WMPLib (Windows Media Player Library)

## Important to Note

The project title is still AngeloTraverso_ST10081927_PROG7312_POE_PART1. Due to the complication I would have had changing that name, it will remain the same. However, this submission is for the Final POE (Part 3).

## Installation

### Option 1

1. Unzip folder "AngeloTraverso_ST10081927_PROG7312_POE_PART1"
2. Open folder "AngeloTraverso_ST10081927_PROG7312_POE_PART1"
3. Double click the Visual Studio solution "DeweyDecimal_Latest.sln"

### Option 2

1. Unzip folder "AngeloTraverso_ST10081927_PROG7312_POE_PART1"
2. Open Visual Studio
3. Choose "Open a project or solution"
4. Locate folder "AngeloTraverso_ST10081927_PROG7312_POE_PART1" from the unzipped folder
5. In "AngeloTraverso_ST10081927_PROG7312_POE_PART1" folder, select "DeweyDecimal_Latest.sln" file to launch the code into your IDE

### Option 3 (Github)

1. Open [this link](https://github.com/Angelo-Traverso/AngeloTraverso_ST10081927_PROG7312_PART1.git) in your browser
2. Choose the green button “Code”
3. Copy the URL displayed
4. In your command prompt, use the “cd” command to change directories to where you want to store this application.
5. Once you're in your desired file, type `git clone https://github.com/Angelo-Traverso/AngeloTraverso_ST10081927_PROG7312_PART1.git` followed by the `Enter` key.
6. Follow Option 1 step 2.

## Usage Guide

- Launch the game and select your game (Replace Books or Identify Areas).
- In "Identify Areas," match the left column items with the right column items.
- Click the “Play” button to start the game.
- Click the “Restart” button to play again and try to beat your best time.

Note: You can only “Win” a game by matching all 4 questions correctly.

## Usage Guide (Finding Call Numbers Game)

- When the game launches, click “Ready” to start your game.
- Each game has 3 rounds, each delving deeper into the Dewey Decimal system.
- Round 1 is blue and displays top-level options (100, 200, etc.)
- Round 2 is orange and displays second-level options (110, 120, etc.)
- Round 3 is pink and shows all lowest levels as options (111, 112, etc.)
- If you get something wrong, you will lose a life.
- Each game gives you only 3 lives.

## Issues Running

Error: “Couldn't process file 'path' due to its being in the Internet or Restricted zone or having the mark of the web on the file”

To fix this issue, navigate to `DeweyDecimal_Latest\Forms` and `\UserControls`. For every file extension ending with “.resx”, right-click the file, go to properties, and find a small check box at the bottom that says “Unblock”. Then run again.

[Link to known issue](https://learn.microsoft.com/en-us/visualstudio/msbuild/errors/msb3821?view=vs-2022&f1url=%3FappId%3DDev16IDEF1%26l%3DEN-US%26k%3Dk(MSBuild.GenerateResource.MOTW)%3Bk(TargetFrameworkMoniker-.NETFramework%2CVersion%253Dv4.8)%26rd%3Dtrue)

## File Structure

All files will be found under "Solution Explorer".
All images are stored in bin/debug/Images.
All models are stored under /Models.
All sound effects are stored in bin/debug/Sound_Effects.

## Predefined Value

A predefined value of 35 seconds is made for your personal best time to make testing easier.

## Class Library

A class library was created to handle the timer and its ticks for the game.

## Github Repo Link

[https://github.com/Angelo-Traverso/AngeloTraverso_ST10081927_PROG7312_PART1.git](https://github.com/Angelo-Traverso/AngeloTraverso_ST10081927_PROG7312_PART1.git)

## Student Information

- Name: Angelo
- Surname: Traverso
- Student Number: ST10081927
- Subject: Programming 3B
- Subject Code: PROG7312
- Part: 2

## REFERENCES

- I made use of an external library called `Control.Draggable()`: [Control.Draggable()](https://github.com/intrueder/Control.Draggable)
- I also made use of ChatGPT to help refine some of my code and UI.
- CSV file of Dewey Decimal was used from OCLC (2005).
- OCLC (2005) Terms, OCLC. Available at: https://www.oclc.org/research/activities/browser/terms.html (Accessed: 20 November 2023).
