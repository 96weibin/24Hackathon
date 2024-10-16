
## Create database  
1. Connect to SQL Server
2. Create empty database "KnowledgeBase"  
3. Set the Change Tracking of database to be True.

## Run db schema script  
```
1. \Models\Model1.edmx.sql
2. \DbScripts\initdb.sql
```


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

```
## Log on the system in different role, the defualt password is 123456
# Customer
1. SamBush
2. MillsShone
# RND 
1. JohnDoe
2. DavidShon
3. KamaGreze
# Support
1. GraceTim
2. RivasRaul

-- GraceTime is support for APC
-- RivasRaul is support for Hysys
#NOTE:
1. We only setup Azure question and answering knowledge language for product APC and Hysys for this demo. So try ask question for this two products only.
2. To simplify the project, we associcate  a default support for each product.