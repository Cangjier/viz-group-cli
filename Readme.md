# 使用教程
## Server
``` bash
VizGroup.CLI server --port 8080 --cert ssl.pem --cert-password 123456
```

## Client
``` bash
VizGroup.CLI client --port 8080 --share-server-url-prefix "http://192.168.0.111:8080/" --database-path Agent/Agent.db --plugins-directory Agent/Plugins
```


