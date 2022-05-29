# Support-Vector-Machine
Solving the problem of optimizing a support vector machine using an evolutionary algorithm for the purpose of classifying cars.

## Scope
The basic issue of the project is the classification of a set of cars that is divided into four categories: unacceptable, acceptable, good and very good, each category having a certain number of instances in the training set, respectively testing. Moreover, each instance contains a number of 6 attributes through which the classification can be made: buying, maint, doors, persons, lug_boot, safety.

## Solution
The solution proposed to solve the above-mentioned problem is an application that use an evolutionary algorithm to determine the parameters of the SVM, which are subsequently required in the classification process. After determining the equation characteric of SVM (alpha, bias, W parameters), the equation that separates the plane into two regions, the classification process can begin.

Are you up to the challange?

## Steps to run
This project was developed in the C# language within the Visual Studio development environment. You can also use any other IDE that allows you to run source files.
Below is a useful link for downloading Visual Studio 2022.

[Download Visual Studio](https://visualstudio.microsoft.com/downloads/)

The project has a modular structure and the order of execution of the source files is important because succesively intermediate data files are generated, starting from the files with training and testing data: dataSet.data and dataTest.data.

### Execution flow
1.
2.
3.
