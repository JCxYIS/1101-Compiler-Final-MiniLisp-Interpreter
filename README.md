# MiniLisp Interpreter
###### 1101 Compiler Final Project

> A hand-craft Mini-Lisp (subset of LISP) interpreter written in C#

## Usage
- Simply open the program, paste your lisp code and press `Ctrl + Z`,
- or you can pass in the path to the lisp code file inro the program (e.g. `"./output.exe testcase/test_data/03_1.lsp"`)

### Available Args
`--debug` : Debug Mode
`--help`  : Print help and quit
`--once`  : Run once only
`--quiet` : Quiet Mode
(file path) : Read Mini-Lisp code from file

## Tasks

#### Basic Features

- [x] 1. Syntax Validation Print “syntax error” when parsing invalid syntax *(10%)*
- [x] 2. Print Implement print-num statement *(10%)*
- [x] 3. Numerical Operations Implement all numerical operations *(25%)*
- [x] 4. Logical Operations Implement all logical operations *(25%)*
- [x] 5. if Expression Implement if expression *(8%)*
- [x] 6. Variable Definition Able to define a variable *(8%)*
- [x] 7. Function Able to declare and call an anonymous function *(8%)*
- [x] 8. Named Function Able to declare and call a named function *(6%)*

#### Bonus Features
- [ ] 1. Recursion Support recursive function call *(+5%)*
- [x] 2. Type Checking Print error messages for type errors *(+5%)*
- [ ] 3. Nested Function Nested function (static scope) *(+5%)*
- [ ] 4. First-class Function Able to pass functions, support closure *(+5%)*

##### Note
Currently all number is treated as double, so the test cases might give a different output than the answer.

## Screenshots
![](https://i.imgur.com/OEqYKiY.png)

![](https://i.imgur.com/HFgVupk.png)

![](https://i.imgur.com/COJCmsZ.png)