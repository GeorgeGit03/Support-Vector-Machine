# Support-Vector-Machine
Solving the problem of optimizing a support vector machine using an evolutionary algorithm for the purpose of classifying cars.

## Scope
The basic issue of the project is the classification of a set of cars that is divided into four categories: unacceptable, acceptable, good and very good, each category having a certain number of instances in the training set, respectively testing. Moreover, each instance contains a number of 6 attributes through which the classification can be made: buying, maint, doors, persons, lug_boot, safety.

## Solution
The solution proposed to solve the above-mentioned problem is an application that use an evolutionary algorithm to determine the parameters of the SVM, which are subsequently required in the classification process. After determining the equation characteric of SVM (alpha, bias, W parameters), the equation that separates the plane into two regions, the classification process can begin.

#### Are you up to the challange?

## Steps to run
This project was developed in the C# language within the Visual Studio development environment. You can also use any other IDE that allows you to run source files.
Below is a useful link for downloading Visual Studio 2022.

[Download Visual Studio](https://visualstudio.microsoft.com/downloads/)

The project has a modular structure and the order of execution of the source files is important because succesively intermediate data files are generated, starting from the files with training and testing data: **dataSet.data** and **dataTest.data**.

# IMPORTANT 
## A very important aspect is setting the correct paths to the data sets in each source file!

### Execution flow

1. The **'Quantification.sln'** source file, which receives the **dataSet.data** file as input, is used to generate the quantification of the instances in each of the four classes. The resulting files are:
- **dataSetQuantifiedUnacc.data**
- **dataSetQuantifiedAcc.data**
- **dataSetQuantifiedGood.data**
- **dataSetQuantifiedVgood.data**

![Writing](https://user-images.githubusercontent.com/67193200/170867923-6d3afb39-0e29-4b88-8dba-af5e222385fe.JPG)


2. The **'Quantification.sln'** source file from 'Quantification_Of_Test_Data' folder, which receives the **dataTest.data** file as input, is used to generate the quantification of the test instances. The resulting file is:
- **dataTestQuantified.data**

![WritingTest](https://user-images.githubusercontent.com/67193200/170869795-475dd823-9b80-4926-ac55-c3338b6fba47.JPG)


3. The source file **'EvolutionaryAlgorithm.sln'** from each file is executed: 
- GA_Unacc
- GA_Acc
- GA_Good
- GA_Vgood

![AccCalculated](https://user-images.githubusercontent.com/67193200/170870397-be0f218a-e137-4082-9886-0945620fee4e.JPG)

Thus, text files that indicate the degree of belonging of an instance to a certain class by real values are generated:
- classifiedValuesUnacc.data
- classifiedValuesAcc.data
- classifiedValuesGood.data
- classifiedValuesVgood.data

4. Running the source code from the file **'Clasificator.sln'**. The principle of determining the class to which a instance belongs is simple. For example, if the first instace receives a maximum classification value in the **classifiedValuesUnacc.data**, then it is part of the Unacc class. The membership percentage is then determined for each class.

## Results
![Results](https://user-images.githubusercontent.com/67193200/170873051-b68a028e-9162-49e1-b97c-d865b9bbd3c5.JPG)

## Accuracy
The accuracy of the results can be determined as an additional feature by comparing the percentage for each class in the text file car.names, respectively from the results above provided by running the code from the file **'Clasificator.sln'**.

![Distribution](https://user-images.githubusercontent.com/67193200/170873702-79b3b5c2-a3a5-40f7-8b39-578d9b2d81c1.JPG)

## Documentation
For more technical details, I strongly recommend going through the [documentation](https://github.com/GeorgeGit03/Support-Vector-Machine---Car-classification-/blob/main/Documentation.pdf).

Licensed under the [MIT License]().
