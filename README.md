# API网关项目

项目结合Ocelot与Consul之间通讯进行服务发现或手动配置路由转发请求至各个服务，还新增Skywalking支持服务性能监控与追踪，项目提供了多种配置方式。

## 使用配置文件配置Ocelot

完整配置方式，请参照官方文档：https://ocelot.readthedocs.io/en/latest/features/configuration.html

### 手动路由配置示例
``` json
// appsettings.json 配置示例
{
  "ReRoutes": [
    {
      "ServiceName": "UserService",
      "UpstreamPathTemplate": "/userService/{url}",
      "DownstreamPathTemplate": "/api/{url}",
      "DownstreamScheme": "http",
      "LoadBalancerOptions": {
        "Type": "LeastConnection"
      },
      "UpstreamHttpMethod": [ "GET", "POST", "DELETE", "PUT", "OPTIONS" ]
    }
  ],
  "GlobalConfiguration":{}
}
```

### 配置Consul服务发现
``` json
// appsettings.json 配置示例
{
  "ReRoutes": [],
  "GlobalConfiguration": {
    "BaseUrl": null,
    "ServiceDiscoveryProvider": {
      "Host": "127.0.0.1",
      "Port": 8500
    }
  }
}
```

## 结合携程阿波罗配置中心配置

携程Apollo的使用请参考项目：https://github.com/zengqinglei/Creekdream.Configuration.Apollo

### 可将appsetting中的ocelot配置转移至配置中心
``` json
// appsettings.json 配置示例
{
  "apollo": {
    "AppId": "PublicService",
    "MetaServer": "http://127.0.0.1:8080",
    "Namespaces": [ "application" ]
  }
}
```
![image](https://user-images.githubusercontent.com/7374317/44725672-796bdc00-ab08-11e8-9942-5a2ecd6f954c.png)

## 构建Docker镜像与启动项目

### 一、构建Docker镜像

``` bash
git clone https://github.com/zengqinglei/Creekdream.ApiGateway.git
cd Creekdream.ApiGateway/src/Creekdream.ApiGateway
docker build -t registry.cn-shenzhen.aliyuncs.com/creekdream/apigateway:0.1.1 .
```

### 二、 运行Docker镜像

``` bash
docker run -d --name=creekdream-apigateway \
    --restart=always --network=host \
    -e 'ASPNETCORE_ENVIRONMENT=Production' \
    -e 'ASPNETCORE_URLS=http://0.0.0.0:53211' \
    -e 'apollo:AppId=PublicService' \
    -e 'apollo:MetaServer=http://192.168.0.103:8080' \
    registry.cn-shenzhen.aliyuncs.com/creekdream/apigateway:0.1.0
```
