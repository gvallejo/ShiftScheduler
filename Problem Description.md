The Problem
Our customers would like an automated solution to assign employees to shifts. Their current system has a service which provides information about the employees and various schedule rules along with the days employees would like to not be scheduled (Time off requests). Build a web based application which will determine a shift schedule for June 2015 (weeks 23 through 26).
- Users should be able to view the list of employees
- Clicking on an employee name should display a visual representation of the employee's schedule for the month
- Time off requests which violate the shift-rules should be ignored
- Employee specific shift rules override general rules
- In this case, a “shift” is simply 1 day
If you run into anything ambiguous feel free to make a decision on behalf of the customer. Just be sure to make a note of all assumptions you make in your README or code comments.
The problem has been broken down into various features. Implement features 1 and 2. When finished, submit your solution to the API. Package up your code as described in “Submitting Your Results” and describe how you’d go about implementing the remaining features.
Features:
1. Implement the EMPLOYEES_PER_SHIFT rule (ignore time off requests)
2. Take into account employee time off requests
3. Implement the MAX_SHIFTS rule applying the corporate setting (I.E., ignore employee specific settings)
4. Implement the employee specific MAX_SHIFTS override
5. Implement the MIN_SHIFTS rule applying the corporate setting (I.E., ignore employee specific settings)
6. Implement the employee specific MIN_SHIFTS override
Note: The API will return *all* data for the full set of features. As you implement each feature you can ignore the data that does not apply to your feature set thus far.