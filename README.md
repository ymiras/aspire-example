# YmirasCloud - 基于 .NET Aspire 的微服务电商平台

## 项目概述

YmirasCloud 是一个基于 .NET Aspire 构建的现代化微服务电商平台，采用云原生架构设计，集成了 AI 功能、分布式缓存、消息队列和身份认证等企业级特性。

## 🏗️ 系统架构

### 整体架构

```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│     WebApp      │    │     Catalog     │    │     Basket      │
│   (Blazor UI)   │◄──►│   (Product API) │◄──►│  (Cart Service) │
└─────────────────┘    └─────────────────┘    └─────────────────┘
         │                       │                       │
         └───────────────────────┼───────────────────────┘
                                 │
                    ┌─────────────────┐
                    │   AppHost       │
                    │ (Orchestrator)  │
                    └─────────────────┘
```

### 服务组件

#### 🔧 核心服务

- **AppHost**: 应用程序编排器，负责服务发现和依赖管理
- **Catalog**: 产品目录服务，提供产品管理和 AI 搜索功能
- **Basket**: 购物车服务，管理用户购物车状态
- **WebApp**: Blazor 前端应用，提供用户界面

#### 🛠️ 基础设施服务

- **PostgreSQL**: 主数据库，存储产品信息
- **Redis**: 分布式缓存，用于购物车和输出缓存
- **RabbitMQ**: 消息队列，处理服务间通信
- **Keycloak**: 身份认证和授权服务
- **Ollama**: AI 模型服务，提供聊天和向量搜索功能

#### 📦 共享组件

- **ServiceDefaults**: 通用服务配置，包括健康检查、OpenTelemetry 和消息传递

## 🚀 主要功能

### 产品管理

- 产品的 CRUD 操作
- 产品搜索和过滤
- AI 驱动的智能搜索
- 产品价格变更事件通知

### 购物车功能

- 用户购物车管理
- 购物车持久化存储
- 与产品服务的集成
- JWT 身份认证

### AI 集成

- 基于 Ollama 的聊天功能
- 向量化产品搜索
- 智能产品推荐
- 自然语言查询支持

### 用户体验

- 响应式 Blazor UI
- 实时数据更新
- 输出缓存优化
- 流式渲染

## 🛠️ 技术栈

### 后端技术

- **.NET 8**: 主要开发框架
- **ASP.NET Core**: Web API 框架
- **Entity Framework Core**: ORM 框架
- **MassTransit**: 消息传递框架
- **OpenTelemetry**: 可观测性

### 前端技术

- **Blazor Server**: 交互式 Web UI
- **Bootstrap**: UI 组件库
- **SignalR**: 实时通信

### 基础设施

- **Docker**: 容器化部署
- **PostgreSQL**: 关系型数据库
- **Redis**: 内存数据库
- **RabbitMQ**: 消息代理
- **Keycloak**: 身份管理
- **Ollama**: AI 模型服务

## 📋 系统要求

### 开发环境

- .NET 8 SDK
- Docker Desktop
- Visual Studio 2022 或 VS Code
- 至少 8GB RAM（推荐 16GB）

### 运行时要求

- Windows 10/11 或 Linux/macOS
- Docker Desktop 或 Docker Engine
- 至少 4GB 可用内存

## 🚀 快速开始

### 1. 克隆项目

```bash
git clone https://github.com/ymiras/aspire-example.git
cd aspire-example
```

### 2. 启动应用程序

```bash
# 使用 .NET CLI
dotnet run --project AppHost

# 或使用 Visual Studio
# 右键点击 AppHost 项目 -> 设为启动项目 -> F5
```

### 3. 访问应用

- **Web 应用**: https://localhost:5001
- **产品 API**: https://localhost:5002
- **购物车 API**: https://localhost:5003
- **Keycloak 管理**: http://localhost:8080
- **PgAdmin**: http://localhost:5050
- **Redis Insight**: http://localhost:8001
- **RabbitMQ 管理**: http://localhost:15672

## 📁 项目结构

```
aspire-example/
├── AppHost/                 # 应用程序编排器
├── ServiceDefaults/         # 共享服务配置
├── Services/               # 微服务
│   ├── Catalog/           # 产品目录服务
│   └── Basket/            # 购物车服务
├── Portal/                # 前端应用
│   └── WebApp/            # Blazor Web 应用
└── YmirasCloud.sln        # 解决方案文件
```

## 🔧 配置说明

### 环境变量

- `OTEL_EXPORTER_OTLP_ENDPOINT`: OpenTelemetry 导出端点
- `APPLICATIONINSIGHTS_CONNECTION_STRING`: Azure Monitor 连接字符串

### 服务配置

- **Catalog**: 连接 PostgreSQL 数据库和 Ollama AI 服务
- **Basket**: 使用 Redis 缓存和 Keycloak 认证
- **WebApp**: 集成 Catalog 和 Basket 服务

## 🔍 API 文档

### Catalog API

- `GET /products` - 获取所有产品
- `GET /products/{id}` - 获取指定产品
- `POST /products` - 创建新产品
- `PUT /products/{id}` - 更新产品
- `DELETE /products/{id}` - 删除产品
- `GET /products/search/{query}` - 搜索产品
- `GET /products/aisearch/{query}` - AI 搜索产品
- `GET /products/support/{query}` - AI 支持查询

### Basket API

- `GET /basket/{username}` - 获取用户购物车
- `POST /basket` - 更新购物车
- `DELETE /basket/{username}` - 删除购物车

## 🔐 身份认证

项目使用 Keycloak 进行身份认证：

- 领域: `eshop`
- 客户端: `account`
- JWT Bearer Token 认证

## 📊 监控和可观测性

- **健康检查**: `/health` 和 `/alive` 端点
- **OpenTelemetry**: 分布式追踪和指标收集
- **日志**: 结构化日志记录
- **指标**: ASP.NET Core 和 HTTP 客户端指标

## 🧪 测试

### 运行测试

```bash
# 运行所有测试
dotnet test

# 运行特定项目测试
dotnet test Catalog.Tests
```

### API 测试

项目包含 HTTP 文件用于 API 测试：

- `Catalog/Catalog.http`
- `Basket/Basket.http`

## 🚀 部署

### 本地开发

```bash
dotnet run --project AppHost
```

### Docker 部署

```bash
docker-compose up -d
```

### 生产环境

1. 配置环境变量
2. 设置数据库连接字符串
3. 配置 Keycloak 和 Ollama
4. 部署到 Kubernetes 或云平台

## 🤝 贡献指南

1. Fork 项目
2. 创建功能分支 (`git checkout -b feature/AmazingFeature`)
3. 提交更改 (`git commit -m 'Add some AmazingFeature'`)
4. 推送到分支 (`git push origin feature/AmazingFeature`)
5. 打开 Pull Request

## 📄 许可证

本项目采用 MIT 许可证 - 查看 [LICENSE](LICENSE) 文件了解详情。

## 🆘 支持

如果您遇到问题或有疑问：

1. 查看 [Issues](../../issues) 页面
2. 创建新的 Issue
3. 联系项目维护者

## 🔄 更新日志

### v1.0.0

- 初始版本发布
- 基础微服务架构
- AI 集成功能
- 身份认证系统
- 分布式缓存支持

---

**注意**: 这是一个演示项目，展示了 .NET Aspire 的微服务架构能力。在生产环境中使用前，请确保进行适当的安全配置和性能优化。
