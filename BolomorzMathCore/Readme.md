# BolomorzMathCore

Library aiming to implement mathematical structures and their algorithms

!!! Work in progress !!!

# Abstraction

different abstraction layers on how this library can be used:

abstraction layers:
- `number-types`: `Number|Complex`
- `structure-types<Number|Complex>`: `Edge|Vertex|Graph|Function<Number>|Relation<Number>|NMatrix|CMatrix|NVector|CVector...`
- `algorithms<structure<Number|Complex>>`: `Regression<Relation<Number>, Function<Number>> | Dijkstra<Graph, AlgorithmResult<ShortestPath>> ...`
- `visualization<structure<Number|Complex>>`: `BMCCanvas|BMCArc|BMCLine...`

> - own number types -> `structure<OwnType>` -> `algorithms<structure<OwnType>>` -> `visualization<structure<OwnType>>` using base classes
> - additional structures `structure<Number|Complex>`
> - additional algorithms `algorithm<structure<Number|Complex>>`
> - using own visualization layer with/without using `BMCObjects`

# Structure
- **BolomorzMathCore**
    - ---
    - **Basics**: base classes for numbers and algorithms
        - **Complex**: Complex Number
        - **Number**: Real Number
        - **AlgorithmBase**: base class for algorithms with `generics<T, U>` where T is input-type and U is output-type
    - ---
    - **Analysis**: algorithms on relations and functions in a x-y coordinate system
        - **Base**:
            - **FunctionBase**: base class for `functions<T>`: `f(x) = <g{i}(x)>: f(x) is a tuple of n sub-functions g{i}(x) | i from 1 to n`
            - **FunctionString**: string representation of function f(x)
        - **Objects**:
            - **Axis**: axis in x-y coordinate system
            - **Chart**: x-y coordinate system
            - **Function**: relation `f(x) = y`, 1 y for each x
            - **Point**: (x, y)-tuple/point
            - **Series**: series of (x, y)-tuple/points | scatter
        - **Function**:
            - **Constant**:     constant number `f(x) = a`
            - **Exponential**:  `f(x) = a(x) * b(x)^x + c(x)`
            - **Line**:         `f(x) = a(x) + b(x) * x`
            - **Logarithm**:    `f(x) = a(x) + b(x) * ln(c(x) + d(x) * x) + e(x)`
            - **Polynomial**:   `f(x) = sum(f{i}(x): a{i}(x) * x^i)`
            - **Power**:        `f(x) = a(x) * x^b(x) + c(x)`
        - **Algorithms**:
            - **Regression**: finding best-fit-function for a relation (series/scatter): `AlgorithmBase<Relation<Number>, Function<Number>>`
    - ---
    - **GraphTheory**: algorithms on graphs (for now `graph=vertices&edges`)
        - **Objects**:
            - **Edge**: edge connecting two vertices of a graph
            - **Vertex**: point/intersection-element of a graph
            - **Graph**: collection of vertices and edges
        - **Algorithms**:
            - **AlgorithmResult<T>**: base class for algorithm results `T = [ShortestPath, etc]`
            - **BellmanFord**: shortest-path algorithm: `AlgorithmBase<Graph, List<AlgorithmResult<ShortestPath>>>`
            - **Dijkstra**: shortest-path algorithm: `AlgorithmBase<Graph, List<AlgorithmResult<ShortestPath>>>`
    - ---
    - **Linear Algebra**: algorithms on vectors and matrices
        - **Base**:
            - **MatrixBase**: base class for matrices
            - **DeterminantBase**: base class for determinants
            - **VectorBase**: base class for vectors
        - **Objects**:
            - **Matrix**: ComplexMatrix, NumberMatrix
            - **Determinant**: ComplexDeterminant, NumberDeterminant
            - **Vector**: ComplexVector, NumberVector
        - **Algorithms**:
            - **CharacteristicPolynomial**: finding characteristic polynomial of a matrix: `AlgorithmBase<Matrix, List<Coefficients>>`
            - **BairstowAlgorithm**: finding eigenvalues of a matrix with characteristic polynomial: `AlgorithmBase<CharacteristicPolynomial, List<Eigenvalues>>`
            - **HessenbergTransform**: preparing matrix for qr-transform | eliminating most of the upper half above the diagonal: `AlgorithmBase<Matrix, Matrix>`
            - **QR-Transform**: eliminating the upper half above the diagonal to find the eigenvalues of a matrix: `AlgorithmBase<HessenbergTransform, Matrix>`
    - ---
    - **Visualization**: visualization and interaction with elements of this library | generally on a 2d-canvas | prefix BMC for identification
        - **BMCSettings**: settings for different elements
        - **Command**: commands on an element returning a solution //for now not used
        - **Generics**:
            - **Basics**:
                - **BMCPoint**: point on canvas
                - **BMCCanvas**: collection of geometries for all elements
                - **BMCCollection**: collection of geometries for one element
            - **BMCElementBase**: base class for all elements `generic T=[Vertex, Edge, Matrix, Series...]`
            - **BMCGeometryBase**: base class for geometry-objects
        - **Geometry**: different geometries depicting the elements | transformations (translate, rotate, scale, reflect) on those elements
            - **BMCArc**: circle, half-circle...
            - **BMCArrow**: arrow/pointer/direction
            - **BMCLine**: connector
            - **BMCPolygon**: rectangle, triangle...
            - **BMCText**: descriptions, values...
        - **Graph**:
            - **BMCGraph**: interaction for a graph consisting of vertices and edges
            - **BMCVertex**: interaction for a vertex of this graph
            - **BMCEdge**: interaction for an edge of this graph

# Table of Contents

# Number Types

## Complex Number

## Real Number

# Algorithm Base

# Analysis

## Function Base

## Relation Base
TODO: relation base: series etc

## Chart

# Linear Algebra

## Matrices

## Vectors

## Algorithms

### Finding Eigenvalues

### TODO: linear independence of vectors

# Graph Theory

## Base Objects

## Algorithms

### Shortest Path

# Visualization



# OLD
## Matrices

> Matrices are implemented as complex matrix, where each element is a complex number.</br>
> A NxM Matrix is a collection of elements organized in a 2-dimensional structure with N rows and M columns.</br>
> Several algorithms are implemented to find certain properties of a matrix.</br>
> The main part of the algorithm section is to find the complex eigenvalues and eigenvectors of a square matrix.

## Complex Number

> The set of complex numbers `C` extend the set of real numbers `R`, adding an imaginary part.</br>
> Thus a complex number contains a real part `Re` and an imaginary part `Im`.</br>
> Both parts `Re` and `Im` are real numbers, together describing the complex number.</br>
> A complex number is described as `complex = Re + Im * i`, with `i` being the imaginary number.</br>
> There are other ways describing complex numbers, but this description suffices for this implementation.

Therefore the type `Complex` is implemented as follows
```C#
class Complex
{
    double Re;
    double Im;
}
```

### Operations/Properties

> Operations on a complex number or between complex numbers may change `Re` and `Im`,
> but `i` stays constant and is not a factor in those operations, it is only used as representation of the imaginary part.

---
- **Conjugate**:          
conjugating a complex number describes the negation of the imaginary part, resulting in the `Conjugate`.</br>
```
A.Conjugate()   -> Complex: A.Re - A.Im * i;
```
---
- **Square**:     
squaring a complex number means squaring both real and imaginary parts, resulting in a real number.</br>
```
A.Square()      -> Number:  A.Re*A.Re + A.Im*A.Im;
```
---
- **Absolute**:       
the square-root of the square of a complex number is called the `Absolute`, which is also a real number.</br>
```
A.Absolute()    -> Number:  Sqrt(Square(A));
```
---
- **Sign**:               
the sign of a complex number normalizes it to a unit complex number, representing its normalized direction as a complex number.</br>
```
A.Sign()        -> Complex: (A.Re/Absolute(A)) + (A.Im/Absolute(A)) * i;
```
---
- **SquareRoot**:             
the square-root of a complex number finds the complex number `B` in the equation `A = B * B`, where only `A` is known.</br>
```
                            if(A.Im=0 & A.Re<0) 0 - A.Re * i;
A.SquareRoot()  -> Complex: if(A.Im=0 & A.Re>0) A.Re + 0 * i;
                            else Sqrt((A.Re+Absolute(A))/2) + (A.Im/Abs(A.Im))*Sqrt((-A.Re+Absolute(A))/2) * i; 
```
---

### Operators

> Operators are implemented for `Complex x double` and `Complex x Complex`.</br>
> `Complex A; Complex B; double D;`
---
- **Negation**:
```
    -A          -> Complex: (-A.Re) + (-A.Im) * i;
```
---
- **Addition**:         
```
    A + B                   (A.Re+B.Re) + (A.Im+B.Im) * i;
                -> Complex: 
    A + D                   (A.Re+D) + A.Im * i;
```
---
- **Subtraction**:         
```
    A - B                   (A.Re-B.Re) + (A.Im-B.Im) * i;
    A - D       -> Complex: (A.Re-D) + A.Im * i;
    D - A                   (ND-A.Re) + A.Im * i;
```
---
- **Multiplication**:         
```
    A * B                   (A.Re*B.Re-A.Im*B.Im) + (A.Re*B.Im+B.Re*A.Im) * i
                -> Complex:
    A * D                   (A.Re*D) + A.Im * i;
```
---
- **Division**:         
```
    A / B                   ((A.Re*B.Re+A.Im*B.Im)/B.Square) + ((B.Re*A.Im-A.Re*B.Im)/B.Square) * i;
    A / D       -> Complex: (A.Re/D) + (A.Im/D) * i;
    D / A                   (D*A.Re/A.Square) + (D*A.Im/A.Square) * i;
```
---
- **Comparison**:
```
    A = B       -> Bool:    A.Re=B.Re & A.Im=B.Im;
    A > B       -> Bool:    A.Re > B.Re
```
---


## Complex Matrix

> A `NxM` Matrix is a collection of elements organized in a 2-dimensional structure with N rows and M columns.</br>
> Rows and columns `NxM` are called `Dimension` of the matrix.</br>
> An element `a` is described by its row j and column k, `a(jk)`.</br>
> The diagonals `AD` of a matrix `A` are all elements `a(jk)` where `j=k`.</br>

Example Matrix `A 2x2`:
```
-------------------     
|        |        |
| a(1,1) | a(1,2) |
|        |        |
-------------------     AD = [a(1,1), a(2,2)];
|        |        |
| a(2,1) | a(2,2) |
|        |        |
-------------------
```
> In a complex matrix all elements `a(jk)` are [Complex](#complex-number) numbers.

### Special Matrices

> There are certain special matrices with unique properties.</br>
---
1. Quadratic Matrix:        
    Quadratic or square matrices have the property `N=M`, their dimension is described as `NxN` instead.
    - Zero Matrix `O NxN`:             
    All elements `o(jk)` are zero:</br>
    ```
    o(jk): (0 + 0i) | for all combinations (j,k) (j<=N; k<=N)
    ```
    - Identity `I NxN`:         
    All diagonals `ID` are one, rest are zero:</br>
    ```
    i(jk): j=k? (1 + 0i) : (0 + 0i) | for all combinations (j,k) (j<=N; k<=N)
    ```
---
2. Matrix Array:
    Matrix arrays are defined by a 1-dimensional set of elements `E Nx1`.
    - Diagonal `D NxN`:     
    All diagonals `DD` are elements of `E`, rest are zero:</br>
    ```
    d(jk): j=k? e(j) : (0 + 0i) | for all combinations (j,k) (j<=N; k<=N)
    ```
    - Vector Matrix `V Nx1`:        
    Vector matrix are elements of `E`:</br>
    ```
    v(jk): e(j) | for all combinations (j,k) (j<=N; k=1)
    ```
---
3. Rotation Matrix:
    Rotation matrices are defined by index `p & q`, 2 complex numbers `x1, x2` and dimension `N`.</br>
    The result is an Identity `R NxN`, with rules for elements combined by index `p & q`:</br>
    ```
    r(jk): j=k? (1 + 0i) : (0 + 0i) | for all combinations (j,k) (j<=N; k<=N);
    r(pq): (x2/x1) / (1.0 + (x2/x1)(x2/x1)).SquareRoot;
    r(qp): (-1) * (x2/x1) / (1.0 + (x2/x1)(x2/x1)).SquareRoot;
    r(pp) & r(qq): 1.0 / (1.0 + (x2/x1)(x2/x1)).SquareRoot;
    ```
---

### Transformations 

> Transformations can be done on Matrix `A`, resulting in a Matrix `B`.</br>
---
- (A NxM).SubMatrix(j, k):      
    This transformation results in matrix `B N-1xM-1 | N-1xM | NxM-1` depending on input `j & k`.</br>
    If j is non-zero, the j. row of `A` will be removed from `B`.
    If k is non-zero, the k. column of `A` will be removed from `B`.</br>
    All other elements stay the same.
---
- (A NxM).Transpose:
    This transformation results in matrix `B MxN`, swapping elements row/column wise.</br>
    ```
    b(kj): a(jk) | for all combinations (j,k) (j<=N; k<=M)
    ```
---
- (A NxM).Conjugate:
    This transformation results in matrix `B NxM`, [conjugating](#operationsproperties) all elements.</br>
    ```
    b(jk): a(jk).Conjugate | for all combinations (j,k) (j<=N; k<=M)
    ```
---

### Properties

> Properties are defined for matrices.</br>

- Quadratic:
    Quadratic or square matrices have the property `N=M`, their dimension is described as `NxN` instead.


## Determinant

## Algorithms - Finding Eigenvalues

