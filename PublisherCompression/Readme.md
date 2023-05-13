# Publisher Client Compression Testing.
This is a simple test project to test the compression of the Publisher client.

## Usage
- This application is intented to be used in a Cloud VM where we can see the bandwidth saving OOB in the Metrics.
- The application doesn't log the outbound data size, so Cloud VM metrics is the only way to see the compression. 
- Clone the respository.
- Open the solution in Visual Studio and build the project.
- The `Test Data` folder contains the generated `Synthetic` and `Realistic` data files. You can add or remove files based on your test scenario.
- Update the data with which you want to test the compression in the `Test Data` folder.
- Run the application for sometime. (You may want to increase/decrease the number of iterations in the `Program.cs` file based on your test scenario) )
- See the metrics in the Cloud VM to see the bandwidth saving.