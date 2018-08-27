# API网关项目

项目结合Ocelot与Consul之间通讯进行服务发现或手动配置路由转发请求至各个服务。

## 配置项目
提供以下两种配置方式
### 一、 在appsettings.json 中配置Apollo配置中心服务
``` json
{
  "apollo": {
    "AppId": "Creekdream.ApiGateway",
    "MetaServer": "http://127.0.0.1:8080",
    "Namespaces": [ "application" ]
  }
}
```
_在配置中心配置如下图所示：_

![image](https://user-images.githubusercontent.com/7374317/44671589-803b1600-aa58-11e8-810d-06fa56fb8fc5.png)

### 二、 Ocelot 的配置示例，也可参照[官方文档](https://ocelot.readthedocs.io/en/latest/features/configuration.html)
``` json
{
  "ReRoutes": [
    {
      "ServiceName": "UserService",
      "UpstreamPathTemplate": "/userService/{url}",
      "DownstreamPathTemplate": "/api/{url}",
      "DownstreamScheme": "http",
      "UseServiceDiscovery": true,
      "LoadBalancerOptions": {
        "Type": "LeastConnection"
      },
      "UpstreamHttpMethod": [ "GET", "POST", "DELETE", "PUT", "OPTIONS" ]
    },
    {
      "ServiceName": "ProductService",
      "UpstreamPathTemplate": "/productService/{url}",
      "DownstreamPathTemplate": "/api/{url}",
      "DownstreamScheme": "http",
      "UseServiceDiscovery": true,
      "UpstreamHttpMethod": [ "GET", "POST", "DELETE", "PUT", "OPTIONS" ],
      "RateLimitOptions": {
        "ClientIdHeader": "client_id", // 用来识别客户端的请求头，默认是 ClientId
        "QuotaExceededMessage": "Too many requests, are you OK?", // 当请求过载被截断时返回的消息
        "RateLimitCounterPrefix": "ocelot",
        "DisableRateLimitHeaders": false, // Http头  X-Rate-Limit 和 Retry-After 是否禁用
        "HttpStatusCode": 429 // 当请求过载被截断时返回的http status
      },
      "QoSOptions": {
        "ExceptionsAllowedBeforeBreaking": 3,
        "DurationOfBreak": 10000,
        "TimeoutValue": 5000
      },
      "LoadBalancerOptions": {
        "Type": "LeastConnection"
      }
    }
  ],
  "GlobalConfiguration": {
    "BaseUrl": null,
    "ServiceDiscoveryProvider": {
      "Host": "127.0.0.1",
      "Port": 8500
    },
    "HttpHandlerOptions": {
      "AllowAutoRedirect": false,
      "UseCookieContainer": false,
      "UseTracing": false
    }
  }
}
```
## 构建Docker镜像与启动项目
### 一、构建Docker镜像
``` bash

```
### 二、 运行Docker镜像
``` bash

```