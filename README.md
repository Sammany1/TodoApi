# Dashboard Service Overview

- Always return todos with all their subtasks nested.
- Initially, return only dashboard info.
- When a user selects a dashboard, return all todos with their subtasks nested.
- For updates and deletes, perform actions by specific ID endpoint.
- Any dashboard search or sort happens on the client side because the data is already loaded in the client.
- Global search and sort:
  - Since it's a todo app and all the info is simple text, we can load all the data in the client when the user performs search and sort there.
  - We may implement a filter and sort on the server side later, but for now, it can be done on the client side.
