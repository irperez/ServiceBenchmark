ServiceBenchmark
================

see: http://blog.bodurov.com/Web-Api-with-async-await-vs-ServiceStack/

Comparing speed of "Service-Frameworks":
- ServiceStack
- Web API

master branch: claim that ServiceStack is 3 times faster than web api

modified branch: demonstarates that just by properly using the clients, the gain drops to <10%

modified-with-client-async-await branch: shows that if using async await with web api makes ServiceStack slower
