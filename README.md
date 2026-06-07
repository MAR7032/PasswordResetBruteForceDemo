# Password Reset Brute Force Demo

## Project Information

- Project name: Password Reset Brute Force Demo
- Subject: Object-Oriented Programming
- Assignment: Assignment no. 12
- Language: C#
- Application type: WPF desktop application
- IDE used: Visual Studio
- Version control: GitHub

## Repository Link

- GitHub: https://github.com/MAR7032/PasswordResetBruteForceDemo

## Project Purpose

- This project demonstrates a password reset brute-force simulation.
- It is created for educational purposes only.
- The program runs locally on a generated test password.
- It should not be used on real accounts or real systems.

## Main Features

- Random password generation
- SHA256 hashing with static salt
- Single-thread brute-force attack
- Multi-thread brute-force attack
- Stop button with cancellation logic
- Progress bar, elapsed time, found password, and performance log
- Performance comparison between single-thread and multi-thread attack

## Technologies Used

- C#
- WPF
- .NET
- Visual Studio
- GitHub

## GitHub Commit Development Order

| Commit No. | Commit Topic | What Was Added |
|---|---|---|
| Commit 1 | Add .gitattributes, .gitignore, and README.md | Basic repository files |
| Commit 2 | Add project files | Initial Visual Studio project |
| Commit 3 | Added basic graphical user interface | WPF interface |
| Commit 4 | Added password generator class | Random password generation |
| Commit 5 | Added SHA256 hash service | Hashing with static salt |
| Commit 6 | Connected password generation to GUI | Generate button logic |
| Commit 7 | Added password validator class | Password checking logic |
| Commit 8 | Added brute force generator class | Combination generation |
| Commit 9 | Added single-thread brute force attack | Single-thread attack |
| Commit 10 | Added multi-thread brute force attack | Multi-thread attack |
| Commit 11 | Added performance comparison log | Time and attempt comparison |
| Commit 12 | Added stop button cancellation logic | Safe stop logic |
| Commit 13 | Updated README with project details | Final documentation |

## Class Summary

### MainWindow

- Controls the graphical user interface.
- Handles button click events.
- Shows password, hash, progress, elapsed time, found password, and log.
- Related commits: Commit 3, 6, 9, 10, 11, 12

### PasswordGenerator

- Generates a random test password.
- Password length is 4 or 5 characters.
- Related commit: Commit 4

### HashService

- Creates a SHA256 hash.
- Adds a constant static salt before hashing.
- Related commit: Commit 5

### PasswordValidator

- Hashes each guessed password.
- Compares the guessed hash with the target hash.
- Related commit: Commit 7

### BruteForceGenerator

- Generates possible password combinations.
- Keeps generation separate from validation.
- Related commit: Commit 8

### BruteForceAttackService

- Runs the single-thread and multi-thread brute-force attacks.
- Uses CPU cores minus one for multi-threading.
- Supports cancellation logic.
- Related commits: Commit 9, 10, 12

### AttackResult

- Stores whether the password was found.
- Stores found password, attempts, and elapsed time.
- Related commit: Commit 9

### PerformanceLogger

- Compares single-thread and multi-thread results.
- Shows time difference and attempt counts.
- Related commit: Commit 11

## How the Application Works

- User clicks `Generate Password`.
- The application generates a short test password.
- The password is hashed using SHA256 with a static salt.
- User starts single-thread or multi-thread attack.
- The brute-force algorithm starts from length 1 and checks combinations up to length 6.
- When the correct password is found, the result is shown.
- The application displays elapsed time, attempts, found password, and performance comparison.

## Educational Note

- This project is only for learning.
- It demonstrates object-oriented programming, hashing, brute-force logic, WPF GUI, GitHub commits, and multi-threading.
