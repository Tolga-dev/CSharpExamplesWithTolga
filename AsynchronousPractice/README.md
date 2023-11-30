### Async and Await

to boost performance and improve the user experience, one such method is employing multithreading

 * single thread
 * multi thread


### Asynchronous programming patterns

.net provides three patterns for performing asynchronous operations:
* Task based - tap
  * async and await
  * IT IS RECOMMENDED ASYNCHRONOUS
  * based task and task<TRESULT>
    * Async and Await
      * async marked a method asynchronous means it can run in the bg while another code executes
      * Async is a signature allows us to use await keyword
      * Await is used to pause the execution of a method and asynchronously wait for a task to finish
      
* event based - eap
  * it will trigger a completed event when the task is completed
* asynchronous programming model - apm
  * begin and end methods
  
Apm and Eap is recommended no longer. 




SOURCES

* https://learn.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/
* https://stackify.com/c-threading-and-multithreading-a-guide-with-examples/
* https://www.c-sharpcorner.com/article/async-and-await-in-c-sharp/
* https://dotnettutorials.net/lesson/async-and-await-operator-in-csharp/
* https://auth0.com/blog/introduction-to-async-programming-in-csharp/#Asynchronous-Programming-Patterns
