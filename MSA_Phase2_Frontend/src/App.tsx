import axios from "axios";
import { useState } from "react";
import SearchIcon from "@mui/icons-material/Search";
import TextField from "@mui/material/TextField";
import "./App.css";
import { Box, Button, Grid, Paper, Skeleton } from "@mui/material";

function App() {
  const [number, setNumber] = useState("");
  const [numberInfo, setNumberInfo] = useState("");

  const NUMBER_API_URL = "http://numbersapi.com/";
  return (
    <div>
      <div className="search-field">
        <h1>Number Trivias</h1>
        <div style={{ display: "flex", justifyContent: "center" }}>
          <TextField
            id="search-bar"
            className="text"
            value={number}
            onChange={(prop) => {
              setNumber(prop.target.value);
            }}
            label="Enter any number..."
            variant="outlined"
            placeholder="Search..."
            size="medium"
          />
          <Button
            onClick={() => {
              search();
            }}
          >
            <SearchIcon style={{ fill: "blue" }} />
            Search
          </Button>
        </div>
      </div>

      <div
          id="pokemon-result"
          style={{
            maxWidth: "800px",
            margin: "0 auto",
            padding: "100px 10px 0px 10px",
          }}
      >
          <Paper sx={{backgroundColor: "#F0F8FF"}}>
            <Grid
              container
              direction="row"
              spacing={5}
              sx={{
                justifyContent: "center",
              }}
            >
              <Grid item>
                <Box>
                  <div>
                    <h1>
                      {numberInfo}
                    </h1>
                  </div>
                </Box>
              </Grid>
              <Grid item>
              </Grid>
            </Grid>
          </Paper>
        </div>
    </div>
  );

  function search() {
    axios.get(NUMBER_API_URL + number).then((res) => {
      setNumberInfo(res.data);
    });
  }
}

export default App;