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

The project has a modular structure and the order of execution of the source files is important because succesively intermediate data files are generated, starting from the files with training and testing data: **dataSet.data** and **dataTest.data**.

# IMPORTANT 
## A very important aspect is setting the paths to the data sets!

### Execution flow

1. The 'Quantification' source file, which receives the dataSet.data file as input, is used to generate the quantification of the instances in each of the four classes. The resulting files are:
- dataSetQuantifiedUnacc.data
- dataSetQuantifiedAcc.data
- dataSetQuantifiedGood.data
- dataSetQuantifiedVgood.data

![Writing](https://user-images.githubusercontent.com/67193200/170867923-6d3afb39-0e29-4b88-8dba-af5e222385fe.JPG)


2.
3.
