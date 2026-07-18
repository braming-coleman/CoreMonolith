# Configure SSH for GIT


## 1. Generate a new SSH key pair

You can use the below command via PowerShell. **Make sure to add a passphrase!** 

> *Skip to **2.** if you already have a key pair.*

``` PowerShell
ssh-keygen -t rsa -b 4096 -C "your_email@example.com"
```


## 2. Add key to ``ssh-agent``

To have the key handshake handled in the background you need the ``ssh-agent``.

1. Make sure the ``shh-agent`` is running by executing the below **in a new admin elevated PowerShell window:**

```PowerShell
# start the ssh-agent in the background
Get-Service -Name ssh-agent | Set-Service -StartupType Manual
Start-Service ssh-agent
```

2. Then execute the following in a **PowerShell window without elevated permissions**

```PowerShell
ssh-add "[Full path to your SSH private key]"
```

## 3. Test SSH connection

This is to make sure your SSH connection works and to add the server to your known hosts list.

```PowerShell
ssh -T git@github.com
```
------

### Resources:
 - https://learn.microsoft.com/en-us/azure/devops/repos/git/use-ssh-keys-to-authenticate?view=azure-devops
 - https://docs.github.com/en/authentication/connecting-to-github-with-ssh