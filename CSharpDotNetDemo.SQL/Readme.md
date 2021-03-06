# SQL SERVER

## Database

#### Creating a Database

```sql
Create Database <DbName>;
```

When a Database is created it creates two file
ext|v|Description
---|---|---
.MDF | Master Data file | Contains Actual Data
.LDF | Log Data File | Transaction Log File, Used to recover database

#### Rename a DB After creating

```sql
Alter Database SampleDB Modify Name = SampleDB2;
Execute sp_renameDB 'OldDbName', 'NewDbName';
```

#### Delete or Drop a Database

Cannot be dropped while in Use, Also cannot drop system database ex: master

```sql
Drop Database <DbName>;
```

## Table

- Tables are used to store data in the database.
- Tables are uniquely named within a database and schema.
- Table contains one or more columns. And each **column has an associated data type** specified after its name that defines the kind of data it can store e.g., integer, character, date and time data, etc.
- Table should have a **primary key** which consists of one or more columns.
- Table may have some more constraints specified in the table constraints section such as `FOREIGN KEY, PRIMARY KEY, UNIQUE and CHECK`.
- A column may have one or more column constraints such as `NOT NULL` and `UNIQUE`.

#### SQL Server CREATE TABLE

```sql
CREATE TABLE Employee
(
    Id INT NOT NULL PRIMARY KEY IDENTITY,
    Email VARCHAR(50) NOT NULL,
    UserName VARCHAR(100),
    DateOfBirth datetime2
)
```

## Data Types

In SQL Server, each column, local variable, expression, and parameter has a associated data type. A data type is an attribute that specifies the type of data that the object can hold: integer data, character data, monetary data, date and time data, binary strings, and so on.

### A. Exact numerics

| Data Type        | Lower Range                        | Upper Range                        | Storage                                                                                                        | Description                                                                                            | Remarks                                                                                     |
| ---------------- | ---------------------------------- | ---------------------------------- | -------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------- |
| **bit**          | 0                                  | 1                                  | 1 byte                                                                                                         | Integer that can be 0, 1, or NULL                                                                      | String values TRUE and FALSE can be converted to bit values: <br>`TRUE = 1`<br>`FALSE = 0`. |
| **tinyint**      | 0                                  | 255                                | 1 Byte                                                                                                         |                                                                                                        |
| **smallint**     | -2^15 (-32,768)                    | 2^15-1 (32,767)                    | 2 Bytes                                                                                                        |                                                                                                        |
| **_int_**        | -2^31 (-2,147,483,648)             | 2^31-1 (2,147,483,647)             | 4 Bytes                                                                                                        |                                                                                                        |
| **bigint**       | -2^63 (-9,223,372,036,854,775,808) | 2^63-1 (9,223,372,036,854,775,807) | 8 Bytes                                                                                                        |                                                                                                        |
| **smallmoney**   | -214,748.3648                      | 214,748.3647                       | 4 bytes                                                                                                        | accurate to a ten-thousandth of the monetary units                                                     | Use a period to separate partial monetary units                                             |
| **money**        | -922,337,203,685,477.5808          | 922,337,203,685,477.5807           | 8 bytes                                                                                                        | accurate to a ten-thousandth of the monetary units                                                     | Use a period to separate partial monetary units                                             |
| **decimal(p,s)** | -10^38 +1                          | 10^38 -1                           | It depends upon precision. <br> 1 – 9 -> 5 bytes <br> 10-19->9 bytes <br> 20-28->13 bytes <br> 29-28->17 bytes | Decimal and numeric are synonyms and can be used interchangeably. They have fixed precision and scale. |
| **numeric(p,s)** | -10^38 +1                          | 10^38 -1                           | It depends upon precision. <br> 1 – 9 -> 5 bytes <br> 10-19->9 bytes <br> 20-28->13 bytes <br> 29-28->17 bytes | Decimal and numeric are synonyms and can be used interchangeably. They have fixed precision and scale. |

> SQL Server does not automatically promote other integer data types (tinyint, smallint, and int) to bigint.

> **p (precision)**
>
> - The maximum total number of decimal digits to be stored.
> - It includes both the left and the right sides of the decimal point.
> - The precision must be a value from 1 through the maximum precision of 38.
> - The default precision is 18.
>
> **s (scale)**
>
> - The number of decimal digits that are stored to the right of the decimal point.
> - This number is subtracted from p to determine the maximum number of digits to the left of the decimal point.
> - Scale must be a value from 0 through p, and can only be specified if precision is specified.
> - The default scale is 0 and so 0 <= s <= p.

### B. Approximate numerics

Approximate-number data types for use with floating point numeric data. Floating point data is approximate; therefore, not all values in the data type range can be represented exactly.

| Data Type    | Lower Range | Upper Range | Storage                                                     | Description | Remarks                               |
| ------------ | ----------- | ----------- | ----------------------------------------------------------- | ----------- | ------------------------------------- |
| **float(n)** | -1.79E+308  | 1.79E+308   | depends upon (n)<br>N(1-24)-> 4 bytes<br>N(25-53)-> 8 bytes |             | The default value of N is 53.         |
| **real**     | -3.40E+38   | 3.40E+38    | 4 bytes                                                     |             | The ISO synonym for real is float(24) |

> **n** is the number of bits that are used to store the mantissa (part after the decimal point) of the float number in scientific notation and, therefore, dictates the precision and storage size.
>
> - If n is specified, it must be a value between 1 and 53.
> - The default value of n is 53.
> - SQL Server treats n as one of two possible values.
>   - If 1<=n<=24, n is treated as 24.
>   - If 25<=n<=53, n is treated as 53.

### C. Date and time

> Use the **time, date, datetime2 and datetimeoffset** data types for new work. These types align with the SQL Standard. They are more portable.
>
> - **time, datetime2 and datetimeoffset** provide more seconds precision.
> - **datetimeoffset** provides time zone support for globally deployed applications.

| Data Type          | Lower Range              | Upper Range                    | Storage    | Description                                                                                                                                                                         | Remarks                                                                                                                                                                                                                                         |
| ------------------ | ------------------------ | ------------------------------ | ---------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **date**           | 0001-01-01               | 9999-12-31                     | 3 bytes    | Default format: YYYY-MM-DD <br>Default value: 1900-01-01<br>Accuracy: One day                                                                                                       |
| **time(n)**        | 00:00:00.0000000         | 23:59:59.9999999               | 3-5 bytes  | Default format: hh:mm:ss[.nnnnnnn]<br>Default value: 00:00:00<br>Accuracy: 100 nanoseconds                                                                                          | Time is without time zone awareness and is based on a 24-hour clock.<br>n= fractional seconds scale, i.e. number of digits for the fractional part of the seconds.<br> _ can be an integer from 0 to 7 <br> _ The default fractional scale is 7 |
| **datetime2**      | 0001-01-01 00:00:00      | 9999-12-31 23:59:59.9999999    | 6-8 bytes  | Default format: YYYY-MM-DD hh:mm:ss[.fractional seconds]<br>Default value: 1900-01-01 00:00:00<br>Accuracy: 100 nanoseconds                                                         |
| **datetimeoffset** | 0001-01-01 00:00:00      | 9999-12-31 23:59:59.9999999    | 8-10 bytes | Default formats: YYYY-MM-DD hh:mm:ss[.nnnnnnn] [{+\|-}hh:mm]<br>Default value: 1900-01-01 00:00:00 00:00<br>Accuracy: 100 nanoseconds <br>Timezone offset is -14:00 through +14:00. | It is similar to a datetime2 data type but includes time zone offset as well.                                                                                                                                                                   |
| **datetime**       | January 1, 1753 00:00:00 | December 31, 9999 23:59:59.997 | 8 bytes    | Default Value 1900-01-01 00:00:00<br>Accuracy: Rounded to increments of .000, .003, or .007 seconds                                                                                 | We should avoid using this data type. We can use Datetime2 instead.                                                                                                                                                                             |
| **smalldatetime**  | 1900-01-01 00:00:00      | 2079-06-06 23:59:59            | 4 bytes    | Default value: 1900-01-01 00:00:00<br>Accuracy: One minute                                                                                                                          |

### D. Character strings

> IMPORTANT! **ntext, text, and image** data types will be removed in a future version of SQL Server. Avoid using these data types. Use **nvarchar(max), varchar(max), and varbinary(max)** instead.

| Data Type              | Lower Range | Upper Range         | Storage                  | Description                                                                                                                                                                                                                                                                                                            | Remarks                                                                                  |
| ---------------------- | ----------- | ------------------- | ------------------------ | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------- |
| **char<br>[(n)]**      | 0 chars     | 8000 Chars          | n bytes                  | Fixed-size string data<br>n defines the string size in bytes<br>1<= n <=8,000.                                                                                                                                                                                                                                         | It provides a fixed-width character data type.                                           |
| **varchar<br>[(n)]**   | 0 chars     | 4000 Chars          | n+2 bytes                | Variable-size string data.<br>n defines the string size in bytes<br>1<= n <=8,000.<br>use **max** to indicate a column constraint size up to a maximum storage of 2^31-1 bytes (2 GB).                                                                                                                                 | It is a variable length character data type. N defines the string size.                  |
| **varchar<br>[(max)]** | 0 chars     | 2^31 chars          | n bytes + 2 bytes ~ 2 GB | Variable-size string data.<br>n defines the string size in bytes<br>1<= n <=8,000.<br>use **max** to indicate a column constraint size up to a maximum storage of 2^31-1 bytes (2 GB).                                                                                                                                 | We should avoid using this data type unless required due to its huge storage requirement |
| **text**               | 0 chars     | 2,147,483,647 chars | n+4 bytes                | Variable-length non-Unicode data in the code page of the server and with a maximum string length of 2^31-1 (2,147,483,647). When the server code page uses double-byte characters, the storage is still 2,147,483,647 bytes. Depending on the character string, the storage size may be less than 2,147,483,647 bytes. | Avoid using this data type, as it will be deprecated in future SQL Server releases.      |

> - Use char when the sizes of the column data entries are consistent.
> - Use varchar when the sizes of the column data entries vary considerably.
> - Use varchar(max) when the sizes of the column data entries vary considerably, and the string length might exceed 8,000 bytes.

> When n isn't specified in a data definition or variable declaration statement, the default length is 1. If n isn't specified when using the CAST and CONVERT functions, the default length is 30.

### E. Unicode character strings

> IMPORTANT! **ntext, text, and image** data types will be removed in a future version of SQL Server. Avoid using these data types. Use **nvarchar(max), varchar(max), and varbinary(max)** instead.

| Data Type                  | Lower Range | Upper Range        | Storage                                                                                                                                                                                                                     | Description                                                                                                                                                          | Remarks                                                                             |
| -------------------------- | ----------- | ------------------ | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------- |
| **nchar<br>[(n)]**         | 0 chars     | 4000 Chars         | 2 times n bytes                                                                                                                                                                                                             | Fixed-size string data.<br>n defines the string size in byte-pairs<br>1<= n <=4,000.<br>The storage size is two times n bytes                                        | It is a Unicode string of fixed width.                                              |
| **nvarchar<br>[(n\|max)]** | 0 chars     | 4000 Chars         | Variable-size string data.<br>n defines the string size in byte-pairs<br>1<= n <=4,000.<br> The storage size is two times n bytes + 2 bytes<br>**max** indicates that the maximum storage size is 2^30-1 characters (2 GB). | Nvarchar is a Unicode string of variable width.                                                                                                                      |
| **ntext**                  | 0 chars     | 1,073,741,823 char | 2 times the string length                                                                                                                                                                                                   | Variable-length Unicode data with a maximum string length of 2^30 - 1 (1,073,741,823) bytes. Storage size, in bytes, is two times the string length that is entered. | Avoid using this data type, as it will be deprecated in future SQL Server releases. |

> - Use nchar when the sizes of the column data entries are consistent.
> - Use nvarchar when the sizes of the column data entries vary considerably.
> - Use nvarchar(max) when the sizes of the column data entries vary considerably, and the string length might exceed 4,000 byte-pairs.

> When n is not specified in a data definition or variable declaration statement, the default length is 1. When n is not specified with the CAST function, the default length is 30.

### F. Binary strings

> IMPORTANT! **ntext, text, and image** data types will be removed in a future version of SQL Server. Avoid using these data types. Use **nvarchar(max), varchar(max), and varbinary(max)** instead.

| Data Type                   | Lower Range | Upper Range         | Storage                                               | Description                                                                                                                                                                                                                                                                                                    | Remarks                                                                             |
| --------------------------- | ----------- | ------------------- | ----------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------- |
| **binary<br>[(n)]**         | 0 bytes     | 8000 bytes          | n bytes                                               | Fixed-length binary data with a length of n bytes, where n is a value from 1 through 8,000. The storage size is n bytes.                                                                                                                                                                                       |
| **varbinary<br>[(n\|max)]** | 0 bytes     | 8000 bytes          | Its storage is the actual length of string + 2 bytes. | Variable-length binary data. n can be a value from 1 through 8,000. max indicates that the maximum storage size is 2^31-1 bytes. The storage size is the actual length of the data entered + 2 bytes. The data that is entered can be 0 bytes in length. The ANSI SQL synonym for varbinary is binary varying. |
| **image**                   | 0 bytes     | 2,147,483,647 bytes |                                                       | Variable-length binary data from 0 through 2^31-1 (2,147,483,647) bytes.                                                                                                                                                                                                                                       | Avoid using this data type, as it will be deprecated in future SQL Server releases. |

> - Use binary when the sizes of the column data entries are consistent.
> - Use varbinary when the sizes of the column data entries vary considerably.
> - Use varbinary(max) when the column data entries exceed 8,000 bytes.

> The default length is 1 when n isn't specified in a data definition or variable declaration statement. When n isn't specified with the CAST function, the default length is 30.

### G. Other data types

| Data Type                   | Lower Range | Upper Range | Storage | Description                                                                                                                                                                                                 | Remarks |
| --------------------------- | ----------- | ----------- | ------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------- |
| **cursor**                  |             |             |         | It is useful for variables or stored procedure OUTPUT parameter referencing to a cursor                                                                                                                     |
| **rowversion**              |             |             |         | It returns automatically generated, unique binary numbers within a database                                                                                                                                 |
| **hierarchyid**             |             |             |         | it is a system data type with variable length. We use it to represent a position in a hierarchy                                                                                                             |
| **uniqueidentifier**        |             |             |         | It provides 16 bytes GUID                                                                                                                                                                                   |
| **sql_variant**             |             |             |         | store values of other data types                                                                                                                                                                            |
| **xml**                     |             |             |         | It is a special data type for storing the XML data in SQL Server tables                                                                                                                                     |
| **Spatial Geometry Types**  |             |             |         | We can use this for representing data in a flat (Euclidean) coordinate system                                                                                                                               |
| **Spatial Geography Types** |             |             |         | We can use Spatial Geography type for storing ellipsoidal (round-earth) data, such as GPS latitude and longitude coordinates. It represents data in a round-earth coordinate system                         |
| **table**                   |             |             |         | It is a special data type useful for storing result set temporarily in a table-valued function. We can use data from this for processing later. It can be used in functions, stored procedures, and batches |

</br>

---

---

</br>

## Constraints

### A. PRIMARY KEY

A primary key is a column or a group of columns that uniquely identifies each row in a table. Primary key for a table by is created using the `PRIMARY KEY` constraint.

#### If Primary key consists of only one column, use `PRIMARY KEY` constraint as a column constraint

```sql
CREATE TABLE Employee
(
    Id INT NOT NULL PRIMARY KEY,
    Email VARCHAR(50),
    UserName VARCHAR(100)
);
```

#### If Primary key has two or more columns, use `PRIMARY KEY` constraint as a table constraint

```sql
CREATE TABLE Employee
(
    Id INT NOT NULL IDENTITY,
    Email VARCHAR(50),
    UserName VARCHAR(100),
    PRIMARY KEY (Id, Email)
)
```

#### In case, a primary key has to added to `TABLE` after its creation, It can be added by using the `ALTER TABLE` statement

```sql
ALTER TABLE Employee
ADD PRIMARY KEY(Id);
```

- Each table can contain **only one primary key** (but can have one or more column).
- All columns that participate in the primary key must be defined as `NOT NULL`.
- SQL Server automatically sets the `NOT NULL` constraint for all the primary key columns if the `NOT NULL` constraint is not specified for these columns.
- The `IDENTITY` property is used for the Id column to automatically generate unique integer values.
- When two column col1 and col2 are set as Primary Key, the values in either of the column can be duplicate, but each combination of values from both columns must be unique.
- SQL Server also automatically creates a unique clustered index (or a non-clustered index if specified as such) when a primary key is created.

### B. FOREIGN KEY

A foreign key is a column or a group of columns in one table that uniquely identifies a row of another table (or the same table in case of self-reference).

Consider below Two tables Employee and Department

```sql
--ForeignKey Table
CREATE TABLE Employee
(
    Id INT NOT NULL IDENTITY,
    Email VARCHAR(50),
    UserName VARCHAR(100),
    DeptId INT NOT NULL,
    PRIMARY KEY (Id, Email),
    CONSTRAINT fk_Dept FOREIGN KEY (DeptId) REFERENCES Department(Id)
)

--PrimaryKey Table
CREATE TABLE Department
{
    Id INT NOT NULL PRIMARY KEY IDENTITY,
    Name VARCHAR(100)
}
```

The **Department** table now is called the `parent table` that is the table to which the foreign key constraint references. The **Employee** table is called the `child table` that is the table to which the foreign key constraint is applied.

The foreign key constraint ensures referential integrity. It means that you can only insert a row into the child table if there is a corresponding row in the parent table.

#### FOREIGN KEY constraint syntax

```sql
CONSTRAINT fk_constraint_name
FOREIGN KEY (column_1, column2,...)
REFERENCES parent_table_name(column1,column2,..)
```

The constraint name is optional therefore it is possible to define a FOREIGN KEY constraint as follows, SQL Server will automatically generate a name:

```sql
FOREIGN KEY (column_1, column2,...)
REFERENCES parent_table_name(column1,column2,..)
```

**FK Referential actions: TODO**

### C. NOT NULL constraint

`NOT NULL` constraints simply specify that a column must not assume the NULL.

- They are always written as column constraints.
- By default, if NOT NULL constraint is not specified, SQL Server will allow the column to accepts NULL.

#### Add NOT NULL constraint to an existing column

1. First, update the table so there is no NULL in the column:

```sql
UPDATE table_name
SET column_name = <value>
WHERE column_name IS NULL;
```

2. Second, alter the table to change the property of the column:

```sql
ALTER TABLE table_name
ALTER COLUMN column_name data_type NOT NULL;
```

#### Removing NOT NULL constraint

To remove the `NOT NULL` constraint from a column, you use the `ALTER TABLE ALTER COLUMN` statement as follows:

```sql
ALTER TABLE table_name
ALTER COLUMN column_name data_type NULL;
```

### D. UNIQUE constraint

SQL Server `UNIQUE` constraints allow to ensure that the data stored in a column, or a group of columns, is unique among the rows in a table.
It ENforces uniqueness of the column i.e. the column couldn't allow any duplicate value.

```sql
CREATE TABLE Employee
(
    Id INT NOT NULL IDENTITY,
    Email VARCHAR(50),
    UserName VARCHAR(100) UNIQUE,
    DeptId INT NOT NULL,
    PRIMARY KEY (Id, Email)
)

CREATE TABLE Employee
(
    Id INT NOT NULL IDENTITY,
    Email VARCHAR(50),
    UserName VARCHAR(100) ,
    DeptId INT NOT NULL,
    PRIMARY KEY (Id, Email),
    CONSTRAINT unique_UserName UNIQUE(UserName)
)
```

#### UNIQUE constraints for a group of columns

```sql
CREATE TABLE Employee
(
    Id INT NOT NULL IDENTITY,
    Email VARCHAR(50),
    UserName VARCHAR(100) ,
    DeptId INT NOT NULL,
    PRIMARY KEY (Id, Email),
    UNIQUE(UserName, Email)
)
```

#### Add UNIQUE constraints to existing columns

When you add a UNIQUE constraint to an existing column or a group of columns in a table, SQL Server first examines the existing data in these columns to ensure that all values are unique. If SQL Server finds the duplicate values, then it returns an error and does not add the UNIQUE constraint.

```sql
ALTER TABLE table_name
ADD CONSTRAINT constraint_name
UNIQUE(column1, column2,...);
```

#### Delete UNIQUE constraints

To define a UNIQUE constraint, you use the ALTER TABLE DROP CONSTRAINT statement as follows:

```sql
ALTER TABLE table_name
DROP CONSTRAINT constraint_name;
```

#### Modify UNIQUE constraints

SQL Server does not have any direct statement to modify a UNIQUE constraint, therefore, you need to drop the constraint first and recreate it if you want to change the constraint.

Behind the scenes, SQL Server automatically creates a `UNIQUE index` to enforce the uniqueness of data stored in the columns that participate in the `UNIQUE` constraint. Therefore, if you attempt to insert a duplicate row, SQL Server rejects the change and returns an error message stating that the UNIQUE constraint has been violated.

**UNIQUE constraint vs. PRIMARY KEY constraint**

- Although both UNIQUE and PRIMARY KEY constraints enforce the uniqueness of data, you should use the UNIQUE constraint instead of PRIMARY KEY constraint when you want to enforce the uniqueness of a column, or a group of columns, that are not the primary key columns.
- Different from PRIMARY KEY constraints, UNIQUE constraints allow NULL. Moreover, UNIQUE constraints treat the NULL as a regular value, therefore, it only allows one NULL per column.
- There can be more than one UNIQUE key constraint in table.

### E. CHECK constraint

It is used to limit the range of value that can be entered in a column.
If the boolean expression is true, then the CHECK constraint allows the value. otherwise it doesn't.
For a Nullable Column it is possible to pass NULL, when inserting a row. The boolean expression evaluated to UNKNOWN, and allows the value.

```sql
ALTER TABLE <table_name>
ADD CONSTRAINT <constraint_name> CHECK <boolean_expression>

ALTER TABLE tblPerson
ADD CONSTRAINT CK_tblPerson_Age CHECK (Age > 0 AND Age < 150);
```

```sql
ALTER TABLE <table_name>
DROP CONSTRAINT <constraint_name>
```

### F. DEFAULT constraint

A Column default can be defined using DEFAULT constraint. It is used to insert a default value in column.
The default value will be added to all new records, if no value id specified, including NULL.
Default is consider only when no value is give. Even If Null is given default value will not take.

**Alter an existing column to add a DEFAULT constraint**

```sql
ALTER TABLE <table_name>
ADD CONSTRAINT <constraint_name>
DEFAULT <default_value> FOR <existing_column_name>

```

**Add a new column with DEFAULT constraint**

```sql
ALTER TABLE <table_name>
ADD COLUMN <column_name>(<data_type>)
CONSTRAINT <constraint_name>
DEFAULT <default_value>

```

**Drop a DEFAULT constraint**

```sql
ALTER TABLE <table_name>
DROP CONSTRAINT <constraint_name>
```

### Cascading Referential Integrity

It allows to define the action MSSQL Server should take when a user attempts to delete or update a key to which an existing foreign Key points.
By default, we get an error and the DELETE or UPDATE statement is rolled back.

**Options when setting up Cascading Referential Integrity Constraint**
It Specifies that if an attempt is made to delete or update a row with a key referenced by foreign keys in existing rows in other tables,

1. **No Action** An error is raised and the DELETE or UPDATE is rolled back (It is default behvaiour).
2. **Cascade** All row containing that foreign key are alos deleted or updated.
3. **Set NULL** All row containing that foreign key are set to NULL.
4. **Set Default** All row containing that foreign key are set to default values.

### Identity Column

If a coulmn is marked as Identity column, Then value of that that column are automatically generated When a new row is inserted into the table

```sql
CREATE TABLE tblPerson
(
    PersonId int IDENTITY(1,1),
    Name NVARCHAR(50)
)
```

NOTE: Seed and Incremental value are optional, If they are not specified both of them default to 1.

**To Explicetly supply a value of Identity Column**

1. Torn ON Identity insert `SET Identity_Insert tblPerson ON`
2. In Insert query provide the value for Identity Column.
3. Turn OFF Identity insert `SET Identity_Insert tblPerson OFF`

**To Reset the value of Identity Column**
This may be required if rows are deleted from table.

```sql
DBCC CHECKIDENT('tblPerson', RESEED, 0)
```

**Get Last Generated Identity Value**

1. SCOPE_IDENTITY() - Same session/connection and same scope/executed Statement `Select SCOPE_IDENTITY()`
2. @@IDENTITY - Same session/connection and across the scope/executed Statement `SELECT @@IDENTITY`
3. IDENT_CURRENT('table_name') - Specific table across any session/connection and any scope/executed Statement

</br>

---

---

</br>

## **SQL Statements**

1. **Select**

```sql
Select * from tblPerson;
Select [ID],[Name],[Age] from tblPerson;
```

2. **DISTINCT**

```sql
Select DISTINCT City from tblPerson;
Select DISTINCT Name, City from tblPerson; //Distinct across the selected rows i.e. Name+City will be distinct.
```

3. **WHERE**

```sql
Select * from tblPerson Where City = 'Mumbai';
Select * from tblPerson Where City != 'Mumbai';
Select * from tblPerson Where City <> 'Mumbai';
Select * from tblPerson Where City = 'Mumbai' AND Age>25;
Select * from tblPerson Where (City = 'Mumbai' OR City = 'Delhi') AND Age > 25;
Select * from tblPerson Where  Age IN (20,21,23,24);
Select * from tblPerson Where  Age BETWEEN 20 AND 25;
Select * from tblPerson Where  City LIKE 'M%'; // City Starting with M
Select * from tblPerson Where  Email LIKE '%@%'; // Validate Email
Select * from tblPerson Where  Email NOT LIKE '%@%'; // Not Validate Email
Select * from tblPerson Where  Email LIKE '~@%'; // Get All Person who have only 1 character before @ in email
Select * from tblPerson Where  Name  LIKE '[MST]%'; // All Person whose name start with M or S or T
Select * from tblPerson Where  Name  LIKE '[^MST]%'; // All Person whose name DONOT start with M or S or T


```

**Operators and Wildcards**
| Operator | Purpose | Comment |
| ------------| ----------- | ----------- |
| = | Equal to | |
| != or <> | Not Equal to | |
| > | Grater Than | |
| >= | Greater Than or Equal to | |
| < | Less Than | |
| <= | Less Than or Equal to | |
| IN | Specify a list of values | |
| BETWEEN | Specify a range | It is Inclusive of boundary Condition |
| LIKE | Specify a pattern | |
| NOT | NOT ina list, range, etc.. | |
| % | Specifies zero or more characters | |
| ~ | Specifies exactly one character| |
| [] | Any character within the bracket | |
| [^] | NOT Any character within the bracket | |
| AND | | |
| OR | | |

**Sort the data**

By Default Sorting is done in Ascending order
Can also sort by multiple columns

```sql
Select * from tblPerson Where City='Mumbai' Order By Age;
Select * from tblPerson Where City='Mumbai' Order By Age DESC;
Select * from tblPerson Where City='Mumbai' Order By Name ASC, Age DESC;
```

**Top n / n Percent rows**

```sql
Select TOP 2 * from TblPerson;
Select TOP 2 * from tblPerson Where  City='Mumbai';
Select TOP 2 Name, Age from tblPerson Where City ='Mumbai';
Select TOP 20 Percent * from tblPerson;
Select TOP 1 from tblPerson Order BY Age DESC; // Select Oldest Person
```

4. **GROUP BY**

GROUP BY clause is used to group a selected set or row into set of summary rows by the the value of one or more column or expression.
It is always used in conjunction with one or more aggregate function.

```sql
Select City, SUM(Salary) from tblEmployee GROUP BY City;
Select City, Count(*) from tblEmployee GROUP BY City;
```

**Group BY Multiple Columns**

```sql
Select City, Gender, SUM(Salary) AS TotalSalary, COUNT(ID) AS  [Total Employee]
FROM tblEmployee
WHERE Gender='Male'
GROUP BY City, Gender
ORDER BY City;

Select City, Gender, SUM(Salary) AS TotalSalary, COUNT(ID) AS  [Total Employee]
FROM tblEmployee
GROUP BY City, Gender
ORDER BY City
HAVING Gender='Male';

Select City, Gender, SUM(Salary) AS TotalSalary, COUNT(ID) AS  [Total Employee]
FROM tblEmployee
GROUP BY City, Gender
ORDER BY City
HAVING SUM(Salary) > 500;

```

**Aggregate Functions**
SUM
MIN
MAX

```sql

Select SUM(Salary) from tblEmployee;
Select MIN(Salary) from tblEmployee;
Select MAX(Salary) from tblEmployee;

```

6. **HAVING**

User to filter the data, Output is same as WHere.
It is used after ORDER BY.

**Where**
Filter rows before Aggreation/grouping is done.
From table only male records are retrieved and then only there are grouped
Can be used woth Select Insert and Update Satement.
Aggregate function cannot be used in Where clause, Unless it is is a sub-query contained in a Having Caluse.
**Having**
Filter rows after Aggreation/grouping is done.
From table all records are retrieved , grouped by gender, and then only male group are shown.
Can be used only with Select Statement
aggregate function can be used in having caluse

</br>

---

---

</br>

## **JOIN**

JOIN is used to retrive data from 2 or more reated table. In General Tables are realed to each other using Foreign Key Constarints

> In Most cases `JOIN` are fater than `SUB-QUERIES`. Howerver In case, when only a subset of recordsis needed from a table that is joining with, Sub-Queries can be faster.

There are 3 Types of JOIN

1. INNER JOIN or JOIN
2. OUTER JOIN
   - Left JOIN or Left OUTER JOIN
   - Right Join or Right OUTER JOIN
   - Full Join or Full OUTER JOIN
3. CROSS JOIN

4. INNER JOIN or JOIN
   INNER JOIN returns only the matching rows between both the tables. Non matching rows are eleminated.

```sql
Select Name, Gender, Salary, DepartmentName
From tblEmployee
JOIN tblDepartment
ON tblEmployee.DepartmentId = tblDepartment.Id

```

2. LEFT OUTER JOIN
   LEFT JOIN returns matching rows + non matching rows from ``LEFT tables.

```sql
Select Name, Gender, Salary, DepartmentName
From tblEmployee
LEFT JOIN tblDepartment
ON tblEmployee.DepartmentId = tblDepartment.Id

```

3. RIGHT OUTER JOIN
   RIGHT JOIN returns matching rows + non matching rows from ``RIGHT tables.

```sql
Select Name, Gender, Salary, DepartmentName
From tblEmployee
RIGHT JOIN tblDepartment
ON tblEmployee.DepartmentId = tblDepartment.Id

```

4. FULL OUTER JOIN
   FULL JOIN returns all rows from both left and right the table, including non-matching rows.

```sql
Select Name, Gender, Salary, DepartmentName
From tblEmployee
FULL JOIN tblDepartment
ON tblEmployee.DepartmentId = tblDepartment.Id

```

5. CROSS JOIN
   CROSS JOIN will produce the cartesion product of the 2 tables involved in the join.
   Say, Employee table with 10 rows and Department table with 4 Rows, then cross join between these 2 tables produces (10x4) 40 rows
   It should not have ON Clause

```sql
Select Name, Gender, Salary, DepartmentName
From tblEmployee
CROSS JOIN tblDepartment
```

**Select Non-Matcing rows from LEFT table**

Both below have same result

```sql
Select Name, Gender, Salary, DepartmentName
From tblEmployee
LEFT JOIN tblDepartment
ON tblEmployee.DepartmentId = tblDepartment.Id
Where tblEmployee.DepartmentId IS NULL

Select Name, Gender, Salary, DepartmentName
From tblEmployee
LEFT JOIN tblDepartment
ON tblEmployee.DepartmentId = tblDepartment.Id
Where tblDepartment.Id IS NULL
```

**Select Non-Matcing rows from RIGHT table**

```sql
Select Name, Gender, Salary, DepartmentName
From tblEmployee
RIGHT JOIN tblDepartment
ON tblEmployee.DepartmentId = tblDepartment.Id
Where tblEmployee.DepartmentId IS NULL
```

**Select Non-Matcing rows from BOTH table**

```sql
Select Name, Gender, Salary, DepartmentName
From tblEmployee
FULL JOIN tblDepartment
ON tblEmployee.DepartmentId = tblDepartment.Id
Where tblEmployee.DepartmentId IS NULL OR tblDepartment.Id IS NULL
```

**Self JOIN**

Joining atable with tself is called self join.
Self JOIN is nota different type of JOIN.It can be classified under any type of JOIN.

1. INNER
2. OUTER (Left,Right,Full)
3. CROSS Join

```sql
Select E.Name AS EmployeeName,M.Name As ManagerName
From tblEmployee E
LEFT JOIN tblEmployee M
ON E.ManagerId = M.EmployeeId

Select E.Name AS EmployeeName,M.Name As ManagerName
From tblEmployee E
INNER JOIN tblEmployee M
ON E.ManagerId = M.EmployeeId

Select E.Name AS EmployeeName,M.Name As ManagerName
From tblEmployee E
OUTER JOIN tblEmployee M
```

</br>

---

---

</br>

## NULL

Three ways to replace NULL

1. ISNULL(<expression>,<replacemnt_value>) Function

```sql
Select E.Name AS EmployeeName, ISNULL(M.Name, 'No Manager') As ManagerName
From tblEmployee E
LEFT JOIN tblEmployee M
ON E.ManagerId = M.EmployeeId
```

2. CASE Statement
   CASE WHEN <expression> THEN '<value_for_True>' ELSE '<Value_for_false>' END

```sql
Select E.Name AS EmployeeName,
    CASE WHEN M.Name IS NULL THEN 'No Manager' ELSE M.Name END As ManagerName
From tblEmployee E
LEFT JOIN tblEmployee M
ON E.ManagerId = M.EmployeeId
```

3. COALESCE() Function => returns first non-NULL value

COALESCE(<value1>,<value2>,<value3>,<value<4>,....)

```sql
Select E.Name AS EmployeeName, COALESCE(M.Name, 'No Manager') As ManagerName
From tblEmployee E
LEFT JOIN tblEmployee M
ON E.ManagerId = M.EmployeeId
```

### UNION & UNION ALL Operators

Union & UNION ALL Operator, are used to combine the result set of two or more result queries
For Union & Union All to work, the NUmber, DataType and the order of column in Select Statements should be same.

**UNION**
Combines rows but, `Removes duplicate & sort the data`
It has to perform distinct sort to remove duplicate, Which makes it less faster than UNION ALL

```sql
Select * from tblIndiaCustomers
UNION
Select * from tblUkCustomers
```

**UNION ALL**
Just combines all the rows from both the queries

```sql
Select * from tblIndiaCustomers
UNION ALL
Select * from tblUkCustomers
```

**SORT Result of Union**

ORDER BY should be used only on the last Select Statement

```sql
Select * from tblIndiaCustomers
UNION ALL
Select * from tblUkCustomers
ORDER BY Name
```

**Difference between JOIN & UNION**
Union combines the result set of two or more select queries into single result set (i.e. combines rows from 2 or more tables)
JOIN retrieves data from two or more table based on logical relationship between the table (i.e. combines columns from 2 or more tables)

</br>

---

---

</br>

## **Stored Procedure**

A Stored Procedure is group of T-SQL (Transact SQL) statements.
For User Defined SP Microsoft recommends not to use 'sp*' as a prefix as all the system SP are prefixed with 'sp*'.
It can be execute by just using its name

```sql
CREATE PROCEDURE <procedure_name>
AS
BEGIN
    .....
    .....
END

CREATE PROC <procedure_name>
AS
BEGIN
    .....
    .....
END
```

```sql
CREATE PROCEDURE spGetEmployee
AS
BEGIN
    Select Name , Gender from tblEmployee;
END
```

### **Execute Store Procedure**

1. Select Name of StoredProcedure and click execute button
2. Exec <storedprocedure_name>
3. Execute <storedprocedure_name>
4. Using GUI

### **Alter Stored Procedure**

```sql
ALTER PROCEDURE spGetEmployee
AS
BEGIN
    Select Name , Gender from tblEmployee;
END
```

### **Drop Stored Procedure**

1. DROP PROC <storedprocedure_name>
2. DROP PROCEDURE <storedprocedure_name>
3. Using GUI

### **View the Content of Stored Procedure**

1. sp_helptext <storedprocedure_name>
2. Using GUI

### **Encrypt Stored Procedure**

Cannot retrieve the text

```sql
ALTER PROCEDURE spGetEmployee
WITH ENCRYPTION
AS
BEGIN
    Select Name , Gender from tblEmployee;
END
```

### **Stored Procedure with Parameters**

```sql
CREATE PROC spGetEmployeeByGenderAndDepartment
@Gender nvarchar(20)
@DepartmentId int
AS
BEGIN
SELECT Name, Gender, DepartmentId from tblEmployee
WHERE Gender= @Gender AND DepartmentId =@DepartmentId
END
```

### **Execute Stored Procedure with Parameters**

```sql
Execute spGetEmployeeByGenderAndDepartment 'Male' ,1;
Execute spGetEmployeeByGenderAndDepartment @DepartmentId=1, @Gender='Male';

```

### **Stored Procedure with output parameters**

To create a stored procedure with output parameter, we use the keyword `OUT` or `OUTPUT`

```sql
CREATE PROC spGetEmployeeCountByGender
@Gender nvarchar(20)
@EmployeeCount int OUTPUT
AS
BEGIN
SELECT @EmployeeCount = Count(Id) from tblEmployee WHERE Gender= @Gender
END
```

### **Execute Stored Procedure with Output Parameters**

First a variable needs to be created with same data type as of output to store value

```sql
DECLARE @EmployeeTotal int
EXECUTE spGetEmployeeCountByGender 'Male', @EmployeeTotal output
SELECT @EmployeeTotal

DECLARE @EmployeeTotal int
EXECUTE spGetEmployeeCountByGender  @EmployeeCount = @EmployeeTotal out, @Gender= 'Male'
SELECT @EmployeeTotal
```

###**Return Value of Stored Procedure**
Whenever, a Stored Procedure is executed, it returns an `integer` status variable. Usually Zero indicates Success and non-zero indicates failure.

| RETURN                                      | OUTPUT                                              |
| ------------------------------------------- | --------------------------------------------------- |
| Return value of SP should always be integer | Any Data Type                                       |
| Only one integer value can be returned      | More Than one                                       |
| It is used to convey success or failure     | Use to return any custom value like name, count etc |

```sql
CREATE PROC spGetEmployeeCountByGender
@Gender nvarchar(20)
AS
BEGIN
RETURN (SELECT Count(Id) from tblEmployee WHERE Gender= @Gender)
END
```

```sql
DECLARE @TotalCount int
EXECUTE @TotalCount = spGetEmployeeCountByGender 'Male'
SELECT @TotalCount
```

### **Usefull System Stored Procedure**

1. sp_help <storedprocedure_name / any_db_object> => Get info about any database object. ALT+F1 is keyboard shortcut for same
2. sp_helptext <storedprocedure_name>
3. sp_depends <storedprocedure_name / any_db_object> => Get the details of table on which stored procedure is dependent

</br>

---

---

</br>

### FUNCTIONS

</br>

---

---

</br>

### TEMPORARY TABLE

It is similar to Permanent Table, except Temporary table get created in TempDB and are automatically deleted when they are no longer in use.

**Types of Temporary Table**

### **1. Local Temporary Table**

To Create a Local Temporary Table prefix the table name with single `#`
Random numbers are suffixed at the end of table name
It is available ony for the connection that as created the table
It is Automatically dropped when the connection that has created it is closed
User can explicitly drop the table, DROP TABLE <#table_name>
If it is created inside SP , It will get dropped upon the completion of SP execution.
It is Possible for different connection to create temp table with same name

These tables are found in

- Systen Database => TempDB => Temporary Table => `<Table_Name>`
- `Select Name from tempdb..sysObjects where Name LIKE '#Person'`

```sql
CREATE TABLE #Person
(
    Id int
    Name nvarchar(20)
)

Insert INTO #Person Values (1,'Mike');

Select * from #Person
```

### **2. Global Temporary Table**

It is similar to Local Temp Table, But prefix the table name with two # `##`
No Random numbers are suffixed at the end of table name
They are visible for all the connection/session
It is only destroyed when last connection refrencing the table is closed
Golbal temp table name is Unique accross the connection

```sql
CREATE TABLE ##Person
(
    Id int
    Name nvarchar(20)
)


Insert INTO ##Person Values (1,'Mike');

Select * from ##Person
```

</br>

---

---

</br>

## **Indexes**

Indexes are used by queries to find data from table quickly. It is very similar to Index found on book.
It is Created on table or View.
It can drastically increase the performance of query.
If index is not there th help query, then query engine checks every row in the table, this is called Table Scan, ans is very bad for performance.

Once Index is created on Salary column It help the query, SQL server picks up the row address from index and directly fetches theresord from the table, rather thanscanning each row in table. This is called `Index Seek`.

### **Creating an Index**

1. using GUI
2. Using Query

```sql
CREATE INDEX <IndexName> // Prefered <IX_tableName>
ON <table_name> (<column_name> ASC|DESC)

CREATE INDEX IX_tblEmplyee
ON tblEmployee(SALARY ASC)
```

### **View the Index**

1. GUI
2. sp_helpIndex <table_name> => shows all the index for the given table

## **Delete Index**

DROP INDEX <table_name>.<index_name>

### **Types Of Indexes**

1. Clustered Index
2. Non-Clustered Index
3. Unique
4. Filtered
5. XML
6. FullText
7. Spatial
8. ColumnStore
9. Index with included column
10. Index on computed column

### **1. Clustered Index**

A Clustered Index determines the physical order of data in a table. For this reason, A table can have only one Clustered-Index.
When any columns (like: ID) are marked as primary key, Primay Key constraint create clustered Index automatically on that column, if no clustered index exists in table.

If data is inserted in tabled with not ordered by index column (here: if say inserted data with ID 1,4,2,5,3 , non-sequential order) then it automatically arranged the row based on index when inserted.

It is Analogous to telephone directly, where data is arranged by last name. Similarly in table, data is arranged by clustered index key.
There can be only one Clustered index , but can have multiple column. It is called as `Composite Clustered Index`.

### **Creating an Clustered Index**

```sql
CREATE CLUSTERED INDEX <IndexName> // Preferred Name Format <IX_tableName_ColumnName>
ON <table_name> (<column1_name> ASC|DESC, <column1_name> ASC|DESC)

CREATE CLUSTERED INDEX IX_tblEmplyee_Gender_Salary
ON tblEmployee(Gender DESC, Salary ASC)
```

### **2. Non-Clustered Index**

A non clustered Index is analogous to an index in textbook. The data is stored in one place and index in another place. The index will have pointer/address to storage location of data.

More than one Non-Clusterd index can be created in a table, Since it is stored separately from actual table.
In the Non-Clusterd index itself, the data is tored in Ascending or descending orderof the index key , whichdo not in any way influence the storeage of data in the table

### **Creating an Non-Clustered Index**

```sql
CREATE NONCLUSTERED INDEX <IndexName> // Preferred Name Format <IX_tableName_ColumnName>
ON <table_name> (<column1_name> ASC|DESC, <column1_name> ASC|DESC)

CREATE NonClustered INDEX IX_tblEmplyee_Gender
ON tblEmployee(Gender DESC)
```

| Clustered Index                                                                      | Non-Clustered Index                                            |
| ------------------------------------------------------------------------------------ | -------------------------------------------------------------- |
| Only one per table                                                                   | more than one per table                                        |
| faster than Non-Clustered Index                                                      | Slower tha Clustered Index, as it has to lookup in 2 table     |
| Determines storage order in the table itself, So does not need additional disk space | storage separately from table,So need additional storage space |

### **3. Unique Index**

Unique Index is used to enforce uniqueness of keyvalues in index.
By default Primary Key creates a UNIQUE clustered Index. As it uses it behind the Scene. On deleting UNIQUE cluster Index, it also removed Primay Key Constraint.
UNIQUE Constraint by default creates a UNIQUE Non-clustered Index.

Uniqueness is a property of index, and both Clustered Index and Non-Clustered Index can be UNIQUE

### **Creating an UNIQUE Clustered/Non-Clustered Index**

```sql
CREATE UNIQUE NONCLUSTERED INDEX <IndexName> // Preferred Name Format <UIX_tableName_ColumnName>
ON <table_name> (<column1_name> ASC|DESC, <column1_name> ASC|DESC)

CREATE Unique NonClustered INDEX UIX_tblEmplyee_Firstname_Lastname
ON tblEmployee(Firstname, Lastname)
```

There is no major difference between UNIQUE Index & UNIQUE Constraint, When a UNIQUE Constraint is created it creates UNIQUE Index behind the scenes. Can be both Clustered/Non-Clustered Index.
UNIQUE Index or UNIQUE Constraint cannot be added in an existing table, if table contains duplicate value in the key column.

**Covering Query**: If all the columns requested in the select clause of query, are present in the index, then there is no need to look up in the table again. The requested column can simple be returned from index itself

**Clustered index** always covers a query as all the column rae present in table itself.

**Composite Index** is a index on two or more column. Both Clustered/Non-Clustered Index can be composite. To a certain extent, a composite index can cover the query.

</br>

---

---

</br>

### **VIEW**

A view is nothing more than a saved SQL query. It can also be considers as a virtual table.

- It can be used to reduce the complexity of database schemas.
- It can also be used as a mechanism to implement row or column level security . i.e creates view with where condition
- It can be used to present aggregated data and hide detailed data

- Parameters cannot be passed to view
- WHERE clause can be used with view.
- Rules and default cannot be associated with View
- Order BY is invalid in View Unless TOP or FOR XML is speciifed.
- Views cannot be based on temporary table

### **Creating a View**

```sql
CREATE VIEW <view_ame> // Preferred Name Format <VW_tableName_ColumnName>
AS
    .... // Select query
    ....
```

### **See a View**

1. GUI => In Views folder
2. Query

```sql
Select * from <view_name>
```

### **Modify a View**

```sql
ALTER VIEW <view_name>
AS
    .... // Select query
    ....
```

### **Delete a View**

```sql
DROP VIEW <view_name>
```

### **Updatable Views**

Similar to Select \* from View

It is possible to Update View, Insert into View, Delete from view, which makes the changes in under lying table.

If a View is based on multiple tables, and if view is update it may not correctly update the underlying table. To Correctly update the view that is based on more than 1 table, INSTEAD OF triggers are used.

### **Indexed Viwes**

By default View is just a virtual atble and does not store any data.
When a Inedx is created on View, the view get materialized. I.e View is now capable of string data.
In SQL it is called Indexed Viwes, In Oracle It is called as Materialzed View.
TODO:</br>
TODO:</br>
TODO:</br>

</br>

---

---

</br>

## **Triggers**

Triggers can be considerd as special type of Stored Procedure that executes automatically in response to an trigerring action.

There are 3 types of trigger

1. **DML Trigger (Data Modification Language)**

   It is Fired Automatically in Response to DML Event (INSERT, UPDATE & DELETE)

   **DML triggers are of 2 types**

   1. **After Trigger (Also called FOR triggers)**

      Fires After the triggering action. INSERT, UPDATE & DELETE statement, causes a after trigger to fire after the respective statement completes execution.
      It provides Special table `inserted` / `deleted` which conatins same data that has been affected. It is only available in context of Trigger.
      In case of Update trigger both specila tables are available. inserted table conatins new data and deleted table conatins old data.

      ```sql
        CREATE TRIGGER <triger_name>
        ON <table_name>
        FOR <INSERT | UPDATE | DELETE>
        AS
        BEGIN
            ....
            ....
        END
      ```

      ```sql
        CREATE TRIGGER tr_tblEmployee_ForInsert
        ON tblEmployee
        FOR INSERT
        AS
        BEGIN
            DECLARE @Id int
            Select @Id =Id from inserted

            insert intotblEmployeeAudit
            Values('New Employee with Id = '+ CAST(@Id as Varchar(5)) + 'is added at' + cast(GetDate() as nvarchar(20)))
        END
      ```

   2. **Instead of Triggers**

      Fires Instaed Of the triggering action. INSERT, UPDATE & DELETE statement, causes a INSTAED OF trigger to INSTAED OF after the respective statement execution.

      ```sql
        CREATE TRIGGER <triger_name>
        ON <table_name>
        Instead Of <INSERT | UPDATE | DELETE>
        AS
        BEGIN
            ....
            ....
        END
      ```

      ```sql
        CREATE TRIGGER tr_tblEmployee_ForInsert
        ON tblEmployee
        Instead Of INSERT
        AS
        BEGIN
            DECLARE @Id int
            Select @Id =Id from inserted

            insert intotblEmployeeAudit
            Values('New Employee with Id = '+ CAST(@Id as Varchar(5)) + 'is added at' + cast(GetDate() as nvarchar(20)))
        END
      ```

2. **DDL Trigger**
3. **Logon Trigger**

## **Error Handling**
