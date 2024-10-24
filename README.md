
## Change IIS Express setting
Open file ".vs\KnowledgeBase\config\applicationhost.config"  
Replace
```xml
<section name="anonymousAuthentication" overrideModeDefault="Deny" />
```
with 
```xml
<section name="anonymousAuthentication" overrideModeDefault="Allow" />
```
Replace
```xml
<section name="windowsAuthentication" overrideModeDefault="Deny" />
```
with 
```xml
<section name="windowsAuthentication" overrideModeDefault="Allow" />
```

## Build web application
Open command line, go to directory: Vue3-WebApplication, run command
```  
npm install
npm run buildTo
```
## Build release web application
Open command line, go to directory: Vue3-WebApplication, run command
```  
npm install
npm run build
run powershell script replaceIndexCshtml.ps1
```

