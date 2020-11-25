# 🔁 Looper

Looper is a C# framework that provides a robust architecture to develop cross-platform desktop applications. The whole idea is about to deliver a performant environment with
the less bloatware possible in order to make applications run blazing fast.



## 💡 How is it powered?

Looper uses WebWindowX to present a webview where it is possible to make an entire application using web technologies. If you are interested in learning more about the whole process check out the <a href="https://github.com/mynameisvasco/WebWindowX">WebWindowX repository</a>. The communication between webview and C# code is assured by a callback mechanism, there is no HTTP or sockets involved.



## 📐 How can I see it working?

There is a sample on this repository that tries to demonstrate a simple use case scenario.



## 🖥 What native features are available

- Notifications, on macOs and Linux (windows soon...).
- Vibrancy aka blur background, on macOs (windows soon...).
- Alerts, confirmation dialogs, file upload picker and prompt, on all platforms.
