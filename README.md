<a href="https://www.twilio.com">
  <img src="https://static0.twilio.com/marketing/bundles/marketing/img/logos/wordmark-red.svg" alt="Twilio" width="250" />
</a>

# IVR Phone Tree: IVR for beginners. Powered by Twilio - ASP.NET MVC

![](https://github.com/TwilioDevEd/ivr-phone-tree-csharp/workflows/NetFx/badge.svg)

An example application implementing an automated phone line using Twilio.

[Read the full tutorial here](https://www.twilio.com/docs/tutorials/walkthrough/ivr-phone-tree/csharp/mvc)!

## Local development

This project is built using the [ASP.NET MVC](http://www.asp.net/mvc) web framework.

1. First clone this repository and `cd` into its directory:
   ```
   git clone git@github.com:TwilioDevEd/ivr-phone-tree-csharp.git
   cd ivr-phone-tree-csharp
   ```

1. Build the solution.

1. Expose your application to the wider internet using [ngrok](http://ngrok.com). This step
  is important because the application won't work as expected if you run it through
  localhost.

  To start using `ngrok` in our project you'll have execute to the following line in the _command prompt_.

  ```shell
  ngrok http 1112 -host-header="localhost:1112"
  ```

  Keep in mind that our endpoint is:

  ```
  http://<your-ngrok-subdomain>.ngrok.io/ivr/welcome
  ```

  Remember to update the Local.config file with the generated ngrok subdomain.

1. Configure Twilio to call your webhooks.

  You will also need to configure Twilio to call your application when calls are
  received in your [*Twilio Number*](https://console.twilio.com/us1/develop/phone-numbers/manage/active?frameUrl=%2Fconsole%2Fphone-numbers%2Fincoming%3Fx-target-region%3Dus1).
  The voice url should look something like this:

  ```
  http://<your-ngrok-subdomain>.ngrok.io/ivr/welcome
  ```

  [Learn how to configure your Twilio phone number for Programmable Voice](https://www.twilio.com/docs/voice/tutorials/ivr-phone-tree-csharp-mvc#respond-to-the-phone-call).

## Unit Tests

To run the unit tests within Visual Studio, install the NUnit 3 Test Adapter:
https://marketplace.visualstudio.com/items?itemName=NUnitDevelopers.NUnit3TestAdapter

## Meta

* No warranty expressed or implied. Software is as is. Diggity.
* [MIT License](http://www.opensource.org/licenses/mit-license.html)
* Lovingly crafted by Twilio Developer Education.
