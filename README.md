This code implements features 1 and 2.

Overview
	For the implementation of this scheduler I decided to focus on designing a flexible framework library rather 
	than producing a novel algorithm to implement all the features mentioned in the instruction set. I see more 
	business value on preparing the system to support obvious changes such as the addition of more and more rules 
	(in this context rule = feature), than developing an algorithm that will satisfy only a handful of them. 
	The automatic scheduling of workforce is a known problem that has no silver bullet solution.  
	This framework can be used as a basis by developers to implement a family of scheduling algorithms that can be
	changed at runtime. This gives developers the chance to work towards “specialization” of algorithms and invoke 
	them based on customer’s requirements. As time evolves, researchers in this field will produce new approaches 
	that could be easily implemented, encapsulated and added to the company’s arsenal of solutions.  
Features
-	This framework offers a flexible mechanism to implement rules that will be used to validate a generated instance 
	of a schedule. Validation is of great importance when dealing with scheduling problems since it is heavily used in 
	sophisticated intelligent algorithms such as the ones based on Genetic Algorithms.
	
-	Classifies rules in Hard and Soft rules. 

 
Assumptions:
-	The MIN_SHIFTS applies to all Employees. Even if he/she is not working the week that is under the scope of the rule.
-	The only restrictions observed are features 1 and 2. In this context, an employee can be scheduled to work all 
	days of the week or none at all.
	
Notes:
	- The Web application was developed using the DevExpress 14.1.8 family of components for rapid prototyping.
