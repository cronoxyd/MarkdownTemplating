<style>
html, body { font-family: sans-serif; margin: 0; padding: 0; }
h1 { background-color: #0AF; padding: 5px; color: #FFF; }
h2, h3, h4, h5, h6 { color: #0AF; }
table { border-collapse: collapse; }
td { border-top: 1px solid black; padding: 5px; border-bottom: 1px solid black; padding: 5px 10px 5px 10px; }
</style>

# Systems

List count: {{list.Length}}

## List

| Hostname | IP address | Customer |
| - | - |
{{
var tableBody = "";
for (let i = 0; i < list.Length; i++) { 
	let syst = list[i];
	tableBody += `| ${syst.Hostname} | <a href="https://${syst.IPAddress.ToString()}" target="_blank"><code>${syst.IPAddress.ToString()}</code></a> | ${syst.Customer} |\n`;
}
tableBody
}}