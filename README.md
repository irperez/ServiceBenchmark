ServiceBenchmark
================

see: http://blog.bodurov.com/Web-Api-with-async-await-vs-ServiceStack/

Comparing speed of "Service-Frameworks":
- ServiceStack
- Web API
- MVC 6 (.Net Core 1.0 RC1)

<b>master branch</b>: claim that ServiceStack is 3 times faster than web api

<b>modified branch</b>: demonstarates that just by properly using the clients, the gain drops to <10%

<b>modified-with-client-async-await branch</b>: shows that if using async await with web api makes ServiceStack slower
