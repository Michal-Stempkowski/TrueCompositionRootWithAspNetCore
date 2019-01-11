# True Composition Root with ASP .NET Core 2.1 / Clean Architecture's "Main Component"

This solution shows example way of implementing "Main Component" according to rules described in "Clean Architecture" by Robert C. Martin (Uncle Bob).

It is important part of creating creating so called Clean Architecture / Screaming architecture / Onion Architecture / Hexagonal Architecture / Hex architecture / Ports and Adapters or whatever other name that introduces similar idea.

Description of this architecture can be found for example here: [Robert C. Martin's blog entry](https://blog.cleancoder.com/uncle-bob/2011/09/30/Screaming-Architecture.html)


![Example UML](https://yuml.me/diagram/scruffy/class/[User%20Interface%20A]->[ApplicationCore],[User%20Interface%20B]->[ApplicationCore],[Backend%20Infrastructure%20X]->[ApplicationCore],[Backend%20Infrastructure%20Y]->[ApplicationCore],[Main%20Component]->[Backend%20Infrastructure%20Y],[Main%20Component]->[Backend%20Infrastructure%20X],[Main%20Component]->[User%20Interface%20A],[Main%20Component]->[User%20Interface%20B],[Main%20Component]->[ApplicationCore])


## Problem to be solved

So this "written on napkin" repository shows one of approaches to dealing with problem of decoupling dependencies.
It is especially problem in .NET Core, as libraries have transitive dependencies and
usually application Entrypoint is the same place as CompositionRoot and whole UI project.
This causes UI project to: a) see all references, which may not all be hidden, especially as architecture grows bigger;
b) rely on concrete dependencies thus making swapping backend implementation (for example for UI only tests) a little
more troublesome.

## Different workarounds
There are several workarounds for this issue, though most of them may be considered problematic
* PrivateAssets and couple similar approaches require .csproj changes and have problems with keeping track on
all dlls/manifest files/pdbs;
* ILMerge/ILRepack require using powerful external tools that we generally may not require;
* There is some open source projects that try to improve mechanisms of .NET Core (unfortunately right now I do not remember their names);
* We can move Startup out of assembly and try to fix broken things manually (easier with controllers, more troublesome
with embedded resources, views, etc.)
* Some other workarounds (not exhaustive list) that will be more or less problematic depending on what we want to achieve.

## Setting up Screaming Architecture with CompositionRoot

1. Add ApplicationCore project(s) that does not use anything else (there is also Domain Objects project, I have omitted it for sake of diagram's readability).
2. Add all needed "plugin" projects. At least one of them will be .NET MVC Core project.
3. Add CompositionRoot project, move Program (leaving Startup in MVC project is crucial!) from MVC project to it. Wire up all references naturally.

       If you run your web project right now it may load, though you will probably discover
       that assets like static files may not be handled correctly. It is because current
       directory is current's binary directory, so CompositionRoot, not Web Project Folder.
       
       It should however work if you "publish" your project. It is because folder structure is
       flattened then and "wwwroot" appears in the same library as all CompositionRoot and WebApp
       dlls.
4. Add code that checks whether "wwwroot" folder is available in current directory. If it's not, then
it means that solution structure has not been flattened (for example when running from Visual Studio instead
of publishing it) and you should go to Web project (which in my project structure means "go one up
and descend into WebApp directory" ).
5. Decide where you want to initialize all your dependencies. I preferred to register helper "NonUiDependencyRegistry"
class in CompositionRoot only and let it define dependencies to be resolved later inside WebApp startup.
6. Enjoy your nice dependency tree, simple build process, no external dependencies needed.
7. Make sure that all .NET Core features that you are using are not impacted by this workaround. I haven't tested
it with everything (for example hotswap).

I give no guarantee that any of that code should work nor will be held responsible for any damages that it may cause.
Code is delivered free of charge as it is. Use it anyway you like, share this simple solution awesomeness and
promote (but also challenge) clean code ideas like Screaming Architecture wherever you go ;) .
